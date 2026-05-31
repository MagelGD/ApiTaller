using ApiTaller.Domain.Dtos.Portal;
using ApiTaller.Domain.Models;
using ApiTaller.Domain.Interfaces.Repositories.Portal;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Portal
{
    public class PortalRepository : IPortalRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<PortalRepository> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkOrderNotificationService _notificationService;

        public PortalRepository(
            DataContext context, 
            ILogger<PortalRepository> logger, 
            ICurrentUserService currentUserService,
            IWorkOrderNotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
            _notificationService = notificationService;
        }

        private async Task<int?> GetCustomerIdFromUserAsync(CancellationToken cancellation)
        {
            if (!int.TryParse(_currentUserService.UserId, out int userId)) return null;
            var customer = await _context.Customer
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive, cancellation);
            return customer?.Id;
        }

        public async Task<IEnumerable<PortalOrderListDto>> GetMyOrdersAsync(int customerId, CancellationToken ct)
        {
            try
            {
                var targetCustomerId = customerId > 0 ? customerId : (await GetCustomerIdFromUserAsync(ct) ?? 0);
                if (targetCustomerId == 0) return new List<PortalOrderListDto>();

                var orders = await _context.WorkOrder
                    .Include(o => o.Parts)
                    .Include(o => o.Services)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.BrandNavigation)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.VersionNavigation)
                    .Where(o => o.VehicleNavigation.CustomerId == targetCustomerId && o.IsActive)
                    .OrderByDescending(o => o.EntryDate)
                    .ToListAsync(ct);

                return orders.Select(o =>
                {
                    var totalParts = o.Parts.Where(p => p.IsActive && p.IsApproved).Sum(p => p.UnitPrice * p.Quantity);
                    var totalServices = o.Services.Where(s => s.IsActive && s.IsApproved).Sum(s => s.Price);
                    var hasPending = o.Parts.Any(p => p.IsActive && !p.IsApproved) ||
                                    o.Services.Any(s => s.IsActive && !s.IsApproved);

                    return new PortalOrderListDto
                    {
                        Id = o.Id,
                        VehiclePlate = o.VehicleNavigation?.Plate ?? string.Empty,
                        VehicleBrand = o.VehicleNavigation?.BrandNavigation?.Name ?? string.Empty,
                        VehicleVersion = o.VehicleNavigation?.VersionNavigation?.Version ?? string.Empty,
                        EntryDate = o.EntryDate,
                        EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                        Status = o.Status,
                        GrandTotal = totalParts + totalServices,
                        HasPendingApproval = hasPending && (o.Status == "Cotización" || o.Status == "Esperando Aprobación"),
                        VehicleType = o.VehicleNavigation?.VehicleType ?? "moto"
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener órdenes para el cliente {CustomerId}", customerId);
                return new List<PortalOrderListDto>();
            }
        }

        public async Task<IEnumerable<PortalVehicleDto>> GetMyVehiclesAsync(int customerId, CancellationToken ct)
        {
            try
            {
                var targetCustomerId = customerId > 0 ? customerId : (await GetCustomerIdFromUserAsync(ct) ?? 0);
                if (targetCustomerId == 0) return new List<PortalVehicleDto>();

                var vehicles = await _context.Vehicle
                    .Include(v => v.BrandNavigation)
                    .Include(v => v.ModelNavigation)
                    .Include(v => v.VersionNavigation)
                    .Where(v => v.CustomerId == targetCustomerId && v.IsActive)
                    .ToListAsync(ct);

                var result = new List<PortalVehicleDto>();
                foreach (var v in vehicles)
                {
                    var orders = await _context.WorkOrder
                        .Where(o => o.VehicleId == v.Id && o.IsActive)
                        .OrderByDescending(o => o.EntryDate)
                        .ToListAsync(ct);

                    result.Add(new PortalVehicleDto
                    {
                        Id = v.Id,
                        Plate = v.Plate,
                        Brand = v.BrandNavigation?.Name ?? string.Empty,
                        Model = v.ModelNavigation?.Models ?? string.Empty,
                        Version = v.VersionNavigation?.Version ?? string.Empty,
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        TotalOrders = orders.Count,
                        LastOrderStatus = orders.FirstOrDefault()?.Status,
                        LastOrderDate = orders.FirstOrDefault()?.EntryDate,
                        VehicleType = v.VehicleType
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vehículos para el cliente {CustomerId}", customerId);
                return new List<PortalVehicleDto>();
            }
        }

        public async Task<PortalOrderDetailDto?> GetOrderDetailAsync(int orderId, int customerId, CancellationToken ct)
        {
            try
            {
                var targetCustomerId = customerId > 0 ? customerId : (await GetCustomerIdFromUserAsync(ct) ?? 0);
                if (targetCustomerId == 0) return null;

                var order = await _context.WorkOrder
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.BrandNavigation)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.VersionNavigation)
                    .Include(o => o.Parts).ThenInclude(p => p.ProductNavigation)
                    .Include(o => o.Services)
                    .Include(o => o.Evidences)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.VehicleNavigation.CustomerId == targetCustomerId && o.IsActive, ct);

                if (order == null) return null;

                var evidences = order.Evidences
                    .Where(e => e.IsActive)
                    .Select(e => new PortalEvidenceDto
                    {
                        EvidenceType = e.EvidenceType,
                        PhotoUrl = e.PhotoUrl
                    }).ToList();

                var activeParts = order.Parts.Where(p => p.IsActive).ToList();
                var activeServices = order.Services.Where(s => s.IsActive).ToList();

                var approvedPartsTotal = activeParts.Where(p => p.IsApproved).Sum(p => p.UnitPrice * p.Quantity);
                var approvedServicesTotal = activeServices.Where(s => s.IsApproved).Sum(s => s.Price);

                var history = await _context.WorkOrderHistory
                    .Where(h => h.WorkOrderId == orderId && h.IsActive)
                    .OrderByDescending(h => h.CreatedAt)
                    .Select(h => new PortalHistoryDto
                    {
                        Status = h.Status,
                        CreatedAt = h.CreatedAt
                    }).ToListAsync(ct);

                return new PortalOrderDetailDto
                {
                    Id = order.Id,
                    VehiclePlate = order.VehicleNavigation?.Plate ?? string.Empty,
                    VehicleBrand = order.VehicleNavigation?.BrandNavigation?.Name ?? string.Empty,
                    VehicleVersion = order.VehicleNavigation?.VersionNavigation?.Version ?? string.Empty,
                    EntryDate = order.EntryDate,
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                    Mileage = order.Mileage,
                    FuelLevel = order.FuelLevel,
                    Observations = order.Observations,
                    Status = order.Status,
                    VehicleType = order.VehicleNavigation?.VehicleType ?? "moto",
                    VehicleMotorization = order.VehicleNavigation?.CylinderCapacity ?? "",
                    Evidences = evidences,
                    Parts = activeParts.Select(p => new PortalPartDto
                    {
                        Id = p.Id,
                        Name = p.ProductNavigation != null ? p.ProductNavigation.ProductName : p.PartName,
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice,
                        Total = p.UnitPrice * p.Quantity,
                        IsProvidedByCustomer = p.IsProvidedByCustomer,
                        IsApproved = p.IsApproved,
                        WarrantyEndDate = p.WarrantyEndDate
                    }).ToList(),
                    Services = activeServices.Select(s => new PortalServiceDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                        Price = s.Price,
                        IsApproved = s.IsApproved,
                        WarrantyEndDate = s.WarrantyEndDate
                    }).ToList(),
                    History = history,
                    TotalApprovedParts = approvedPartsTotal,
                    TotalApprovedServices = approvedServicesTotal,
                    GrandTotalApproved = approvedPartsTotal + approvedServicesTotal
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalle de orden {OrderId} para el cliente {CustomerId}", orderId, customerId);
                return null;
            }
        }

        public async Task<bool> ApproveOrderItemsAsync(int orderId, int customerId, PortalApproveItemsDto dto, CancellationToken ct)
        {
            try
            {
                var targetCustomerId = customerId > 0 ? customerId : (await GetCustomerIdFromUserAsync(ct) ?? 0);
                if (targetCustomerId == 0) return false;

                var order = await _context.WorkOrder
                    .Include(o => o.Parts)
                    .Include(o => o.Services)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.CustomerNavigation)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.VehicleNavigation.CustomerId == targetCustomerId && o.IsActive, ct);

                if (order == null) return false;

                // Actualizar aprobación de repuestos
                if (dto.Parts != null)
                {
                    foreach (var partDto in dto.Parts)
                    {
                        var part = order.Parts.FirstOrDefault(p => p.Id == partDto.Id && p.IsActive);
                        if (part != null)
                        {
                            part.IsApproved = partDto.IsApproved;
                            part.UpdatedAt = DateTime.Now;
                        }
                    }
                }

                // Actualizar aprobación de servicios
                if (dto.Services != null)
                {
                    foreach (var svcDto in dto.Services)
                    {
                        var svc = order.Services.FirstOrDefault(s => s.Id == svcDto.Id && s.IsActive);
                        if (svc != null)
                        {
                            svc.IsApproved = svcDto.IsApproved;
                            svc.UpdatedAt = DateTime.Now;
                        }
                    }
                }

                // Cambiar estado a Aprobado o En Reparación según corresponda
                order.Status = "Aprobado";
                order.UpdatedAt = DateTime.Now;

                // Registrar auditoría en historial
                var history = new WorkOrderHistory
                {
                    WorkOrderId = order.Id,
                    Status = "Aprobado",
                    Observations = "Presupuesto aprobado de manera personalizada por el cliente desde el portal móvil.",
                    ActionBy = order.VehicleNavigation?.CustomerNavigation?.FirstName ?? "Cliente (Portal)",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                if (int.TryParse(_currentUserService.UserId, out int currentUserId))
                {
                    history.ResponsibleUserId = currentUserId;
                }

                _context.WorkOrderHistory.Add(history);

                var saved = await _context.SaveChangesAsync(ct) > 0;
                if (saved)
                {
                    await _notificationService.NotifyWorkOrderUpdatedAsync(order.Id, targetCustomerId);
                }
                return saved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar ítems para la orden {OrderId} del cliente {CustomerId}", orderId, customerId);
                return false;
            }
        }
    }
}
