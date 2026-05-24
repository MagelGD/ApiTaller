using ApiTaller.Domain.Dtos.Accounting;
using ApiTaller.Domain.Interfaces.Repositories.Accounting;
using ApiTaller.Domain.Interfaces.Services.Accounting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Accounting
{
    public class AccountingService : IAccountingService
    {
        private readonly IAccountingRepository _accountingRepository;
        private readonly ILogger<AccountingService> _logger;

        public AccountingService(IAccountingRepository accountingRepository, ILogger<AccountingService> logger)
        {
            _accountingRepository = accountingRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<MechanicPaymentSettingsDto>> GetPaymentSettingsAsync(CancellationToken ct)
        {
            try
            {
                var rawList = await _accountingRepository.GetMechanicsWithSettingsRawAsync(ct);

                // LÓGICA DE NEGOCIO: Construir DTO final aplicando defaults de negocio
                return rawList.Select(r => new MechanicPaymentSettingsDto
                {
                    Id = r.SettingId,
                    MechanicId = r.MechanicId,
                    MechanicName = r.MechanicName,
                    PaymentType = r.PaymentType ?? "Porcentaje",
                    Value = r.Value ?? 0
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener configuraciones de pago de mecánicos");
                return new List<MechanicPaymentSettingsDto>();
            }
        }

        public async Task<bool> SavePaymentSettingsAsync(MechanicPaymentSettingsDto dto, CancellationToken ct)
        {
            try
            {
                return await _accountingRepository.SavePaymentSettingsAsync(dto, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar configuración de pago para mecánico {Id}", dto.MechanicId);
                return false;
            }
        }

        public async Task<SalesSummaryDto> GetSalesSummaryAsync(DateTime startDate, DateTime endDate, string status, CancellationToken ct)
        {
            try
            {
                var rawOrders = await _accountingRepository.GetWorkOrderSalesRawAsync(startDate, endDate, status, ct);

                // LÓGICA DE NEGOCIO: Calcular totales de ventas
                decimal totalParts = 0;
                decimal totalServices = 0;
                decimal totalDownPayments = 0;
                int ordersCount = 0;

                foreach (var order in rawOrders)
                {
                    totalParts       += order.Parts.Sum(p => p.Quantity * p.UnitPrice);
                    totalServices    += order.Services.Sum(s => s.Price);
                    totalDownPayments += order.DownPayment;
                    ordersCount++;
                }

                return new SalesSummaryDto
                {
                    TotalParts = totalParts,
                    TotalServices = totalServices,
                    TotalSales = totalParts + totalServices,
                    TotalDownPayments = totalDownPayments,
                    OrdersCount = ordersCount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al calcular resumen de ventas");
                return new SalesSummaryDto();
            }
        }

        public async Task<IEnumerable<PendingServiceDto>> GetPendingServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            try
            {
                var rawServices = await _accountingRepository.GetPendingServicesRawAsync(mechanicId, startDate, endDate, ct);

                // LÓGICA DE NEGOCIO: Calcular comisión según tipo de pago configurado
                return rawServices.Select(raw =>
                {
                    decimal commission = 0;
                    if (raw.PaymentType == "Porcentaje")
                        commission = raw.ServicePrice * (raw.ConfiguredValue / 100m);

                    return new PendingServiceDto
                    {
                        ServiceId = raw.ServiceId,
                        WorkOrderId = raw.WorkOrderId,
                        Plate = raw.Plate,
                        CustomerName = raw.CustomerName,
                        ServiceDescription = raw.ServiceDescription,
                        ServicePrice = raw.ServicePrice,
                        CompletedAt = raw.CompletedAt ?? DateTime.Now,
                        CommissionAmount = commission,
                        PaymentType = raw.PaymentType,
                        ConfiguredValue = raw.ConfiguredValue
                    };
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener servicios pendientes para mecánico {Id}", mechanicId);
                return new List<PendingServiceDto>();
            }
        }

        public async Task<bool> SettleServicesAsync(int mechanicId, DateTime startDate, DateTime endDate, decimal totalAmount, List<int> serviceIds, int responsibleUserId, CancellationToken ct)
        {
            try
            {
                return await _accountingRepository.SettleServicesAsync(mechanicId, startDate, endDate, totalAmount, serviceIds, responsibleUserId, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al liquidar servicios para mecánico {Id}", mechanicId);
                return false;
            }
        }

        public async Task<IEnumerable<MechanicSettlementDto>> GetSettlementHistoryAsync(int? mechanicId, CancellationToken ct)
        {
            try
            {
                return await _accountingRepository.GetSettlementHistoryAsync(mechanicId, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de liquidaciones");
                return new List<MechanicSettlementDto>();
            }
        }
    }
}
