using ApiTaller.Domain.Dtos.Accounting;
using ApiTaller.Domain.Interfaces.Repositories.Accounting;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Accounting
{
    public class AccountingRepository : IAccountingRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountingRepository> _logger;

        public AccountingRepository(DataContext context, ILogger<AccountingRepository> logger)
        {
            _context = context;
            this._logger = logger;
        }

        /// <summary>
        /// Retorna la lista de mecánicos activos con su configuración de pago si existe.
        /// </summary>
        public async Task<IEnumerable<MechanicWithSettingsRawDto>> GetMechanicsWithSettingsRawAsync(CancellationToken ct)
        {
            try
            {
                List<Domain.Models.User> mechanics = await _context.User
                    .Include(u => u.UserRoleIdNavigation)
                    .Where(u => u.IsActive && u.UserRoleIdNavigation.Role.Contains("Mecanico"))
                    .ToListAsync(ct);

                Dictionary<int, MechanicPaymentSettings> settings = await _context.MechanicPaymentSettings
                    .Where(s => s.IsActive)
                    .ToDictionaryAsync(s => s.MechanicId, s => s, ct);

                List<MechanicWithSettingsRawDto> result = new List<MechanicWithSettingsRawDto>();
                foreach (Domain.Models.User mechanic in mechanics)
                {
                    settings.TryGetValue(mechanic.Id, out MechanicPaymentSettings? setting);
                    result.Add(new MechanicWithSettingsRawDto
                    {
                        SettingId = setting?.Id ?? 0,
                        MechanicId = mechanic.Id,
                        MechanicName = mechanic.FullName ?? (mechanic.FirstName + " " + mechanic.FirstSurname),
                        PaymentType = setting?.PaymentType,
                        Value = setting?.Value
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener mecánicos con configuraciones de pago");
                return new List<MechanicWithSettingsRawDto>();
            }
        }

        public async Task<bool> SavePaymentSettingsAsync(MechanicPaymentSettingsDto dto, CancellationToken ct)
        {
            try
            {
                MechanicPaymentSettings? setting = await _context.MechanicPaymentSettings
                    .FirstOrDefaultAsync(s => s.MechanicId == dto.MechanicId && s.IsActive, ct);

                if (setting == null)
                {
                    setting = new MechanicPaymentSettings
                    {
                        MechanicId = dto.MechanicId,
                        PaymentType = dto.PaymentType,
                        Value = dto.Value,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = 1
                    };
                    _context.MechanicPaymentSettings.Add(setting);
                }
                else
                {
                    setting.PaymentType = dto.PaymentType;
                    setting.Value = dto.Value;
                    setting.UpdatedAt = DateTime.Now;
                }

                await _context.SaveChangesAsync(ct);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar configuración de pago para mecánico {Id}", dto.MechanicId);
                return false;
            }
        }

        /// <summary>
        /// Retorna órdenes de trabajo con sus partes y servicios para que el servicio calcule totales.
        /// </summary>
        public async Task<IEnumerable<WorkOrderSalesRawDto>> GetWorkOrderSalesRawAsync(DateTime startDate, DateTime endDate, string status, int? mechanicId, string? vehicleType, CancellationToken ct)
        {
            try
            {
                DateTime start = startDate.Date;
                DateTime end = endDate.Date.AddDays(1).AddTicks(-1);

                IQueryable<Domain.Models.WorkOrder> query = _context.WorkOrder
                    .Include(w => w.Parts).ThenInclude(p => p.ProductNavigation)
                    .Include(w => w.Services)
                    .Include(w => w.VehicleNavigation)
                    .Where(w => w.IsActive && w.EntryDate >= start && w.EntryDate <= end);

                if (mechanicId.HasValue && mechanicId.Value > 0)
                {
                    query = query.Where(w => w.Services.Any(s => s.IsActive && s.IsApproved && s.MechanicId == mechanicId.Value));
                }

                if (!string.IsNullOrEmpty(status) && status != "Todos")
                    query = query.Where(w => w.Status == status);

                if (!string.IsNullOrEmpty(vehicleType) && vehicleType != "all" && vehicleType != "Todos")
                {
                    query = query.Where(w => w.VehicleNavigation.VehicleType == vehicleType);
                }

                List<Domain.Models.WorkOrder> orders = await query.ToListAsync(ct);

                Dictionary<int, int> inventoryMap = await _context.Inventory
                    .Where(i => i.IsActive)
                    .ToDictionaryAsync(i => i.ProductId, i => i.StockQuantity, ct);

                return orders.Select(w => new WorkOrderSalesRawDto
                {
                    WorkOrderId = w.Id,
                    Status = w.Status,
                    DownPayment = w.DownPayment,
                    VehicleType = w.VehicleNavigation?.VehicleType ?? "moto",
                    Parts = w.Parts
                        .Where(p => p.IsActive && p.IsApproved && !p.IsProvidedByCustomer)
                        .Select(p => {
                            inventoryMap.TryGetValue(p.ProductId ?? 0, out int stock);
                            return new WorkOrderPartRawDto
                            {
                                ProductId = p.ProductId,
                                Quantity = p.Quantity,
                                UnitPrice = p.UnitPrice,
                                BasePrice = p.ProductNavigation?.Price ?? 0,
                                StockQuantity = p.ProductId.HasValue ? stock : 0
                            };
                        })
                        .ToList(),
                    Services = w.Services
                        .Where(s => s.IsActive && s.IsApproved && (!mechanicId.HasValue || mechanicId.Value <= 0 || s.MechanicId == mechanicId.Value))
                        .Select(s => new WorkOrderServiceRawDto 
                        { 
                            Price = s.Price,
                            MechanicId = s.MechanicId,
                            CompletedAtDate = s.UpdatedAt ?? s.CreatedAt,
                            DateCompleted = (s.UpdatedAt ?? s.CreatedAt).ToString("yyyy-MM-dd")
                        })
                        .ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener datos de ventas");
                return new List<WorkOrderSalesRawDto>();
            }
        }

        /// <summary>
        /// Retorna los servicios pendientes de pago para un mecánico, sin calcular comisiones.
        /// </summary>
        public async Task<IEnumerable<PendingServiceRawDto>> GetPendingServicesRawAsync(int mechanicId, DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            try
            {
                DateTime start = startDate.Date;
                DateTime end = endDate.Date.AddDays(1).AddTicks(-1);

                MechanicPaymentSettings? settings = await _context.MechanicPaymentSettings
                    .FirstOrDefaultAsync(s => s.MechanicId == mechanicId && s.IsActive, ct);

                List<WorkOrderService> services = await _context.WorkOrderService
                    .Include(s => s.WorkOrderNavigation)
                    .Include(s => s.WorkOrderNavigation.CustomerNavigation)
                    .Include(s => s.WorkOrderNavigation.VehicleNavigation)
                    .Where(s => s.IsActive && s.IsApproved && !s.IsPaidToMechanic
                                && s.MechanicId == mechanicId
                                && s.WorkOrderNavigation.IsActive
                                && (s.WorkOrderNavigation.Status == "Terminado" || s.WorkOrderNavigation.Status == "Entregado")
                                && s.WorkOrderNavigation.EntryDate >= start && s.WorkOrderNavigation.EntryDate <= end)
                    .ToListAsync(ct);

                return services.Select(s => new PendingServiceRawDto
                {
                    ServiceId = s.Id,
                    WorkOrderId = s.WorkOrderId,
                    Plate = s.WorkOrderNavigation.VehicleNavigation?.Plate ?? "N/A",
                    CustomerName = s.WorkOrderNavigation.CustomerNavigation != null
                        ? (s.WorkOrderNavigation.CustomerNavigation.FirstName + " " + s.WorkOrderNavigation.CustomerNavigation.LastName).Trim()
                        : "N/A",
                    ServiceDescription = s.Description,
                    ServicePrice = s.Price,
                    CompletedAt = s.UpdatedAt ?? s.CreatedAt,
                    PaymentType = settings?.PaymentType ?? "Porcentaje",
                    ConfiguredValue = settings?.Value ?? 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicios pendientes para mecánico {Id}", mechanicId);
                return new List<PendingServiceRawDto>();
            }
        }

        public async Task<bool> SettleServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, decimal totalAmount, List<int> serviceIds, int responsibleUserId, CancellationToken ct)
        {
            try
            {
                MechanicPaymentSettlement settlement = new MechanicPaymentSettlement
                {
                    MechanicId = mechanicId,
                    SettlementDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    ServicesCount = serviceIds?.Count ?? 0,
                    StartDate = startDate,
                    EndDate = endDate,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = responsibleUserId
                };

                _context.MechanicPaymentSettlement.Add(settlement);
                await _context.SaveChangesAsync(ct);

                if (serviceIds != null && serviceIds.Any())
                {
                    List<WorkOrderService> servicesToPay = await _context.WorkOrderService
                        .Where(s => serviceIds.Contains(s.Id) && s.MechanicId == mechanicId)
                        .ToListAsync(ct);

                    foreach (WorkOrderService service in servicesToPay)
                    {
                        service.IsPaidToMechanic = true;
                        service.PaidToMechanicAt = DateTime.Now;
                        service.MechanicPaymentSettlementId = settlement.Id;
                        service.UpdatedAt = DateTime.Now;
                    }

                    await _context.SaveChangesAsync(ct);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar liquidación de servicios para mecánico {Id}", mechanicId);
                return false;
            }
        }

        public async Task<IEnumerable<MechanicSettlementDto>> GetSettlementHistoryAsync(int? mechanicId, CancellationToken ct)
        {
            try
            {
                IQueryable<MechanicPaymentSettlement> query = _context.MechanicPaymentSettlement
                    .Include(s => s.MechanicNavigation)
                    .Where(s => s.IsActive);

                if (mechanicId.HasValue && mechanicId.Value > 0)
                    query = query.Where(s => s.MechanicId == mechanicId.Value);

                List<MechanicPaymentSettlement> settlements = await query
                    .OrderByDescending(s => s.SettlementDate)
                    .ToListAsync(ct);

                return settlements.Select(s => new MechanicSettlementDto
                {
                    Id = s.Id,
                    MechanicId = s.MechanicId,
                    MechanicName = s.MechanicNavigation?.FullName ?? "N/A",
                    SettlementDate = s.SettlementDate,
                    TotalAmount = s.TotalAmount,
                    ServicesCount = s.ServicesCount,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de liquidaciones");
                return new List<MechanicSettlementDto>();
            }
        }
    }
}
