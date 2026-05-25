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

        public async Task<SalesSummaryDto> GetSalesSummaryAsync(DateTime startDate, DateTime endDate, string status, int? mechanicId, CancellationToken ct)
        {
            try
            {
                var rawOrders = await _accountingRepository.GetWorkOrderSalesRawAsync(startDate, endDate, status, mechanicId, ct);
                var paymentSettingsList = await _accountingRepository.GetMechanicsWithSettingsRawAsync(ct);
                var settingsMap = paymentSettingsList.ToDictionary(s => s.MechanicId, s => s);

                // LÓGICA DE NEGOCIO: Calcular totales y desgloses
                decimal totalParts = 0;
                decimal totalServices = 0;
                decimal totalDownPayments = 0;
                int ordersCount = 0;

                decimal inStockPartsSales = 0;
                decimal outOfStockPartsSales = 0;
                decimal externalQuotesSales = 0;
                decimal partsCost = 0;

                foreach (var order in rawOrders)
                {
                    totalDownPayments += order.DownPayment;
                    ordersCount++;

                    // Repuestos
                    foreach (var part in order.Parts)
                    {
                        var partTotalVal = part.Quantity * part.UnitPrice;

                        if (part.ProductId == null)
                        {
                            // COTIZACIONES EXTERNAS: pase directo (pass-through).
                            // El taller factura exactamente lo que cobra el proveedor.
                            // NO generan margen → se excluyen de totalParts y partsCost.
                            externalQuotesSales += partTotalVal;
                        }
                        else if (part.StockQuantity > 0)
                        {
                            // REPUESTO CON STOCK: genera margen = Venta - CostoBase
                            inStockPartsSales += partTotalVal;
                            totalParts += partTotalVal;
                            partsCost += part.Quantity * part.BasePrice;
                        }
                        else
                        {
                            // REPUESTO BAJO PEDIDO (sin stock): toca comprarlo, pero
                            // el taller puede cobrar un precio mayor al costo → sí genera margen.
                            outOfStockPartsSales += partTotalVal;
                            totalParts += partTotalVal;
                            partsCost += part.Quantity * part.BasePrice;
                        }
                    }

                    // Servicios
                    foreach (var service in order.Services)
                    {
                        totalServices += service.Price;
                    }
                }

                // Calcular Pago a Mecánicos
                decimal mechanicPayout = 0;
                var allServices = rawOrders.SelectMany(o => o.Services).ToList();
                
                // Agrupar servicios por mecánico para calcular su payout
                var groupedByMechanic = allServices.GroupBy(s => s.MechanicId);
                foreach (var mechanicGroup in groupedByMechanic)
                {
                    if (!mechanicGroup.Key.HasValue) continue;
                    var mechId = mechanicGroup.Key.Value;

                    settingsMap.TryGetValue(mechId, out var setting);
                    var paymentType = setting?.PaymentType ?? "Porcentaje";
                    var configValue = setting?.Value ?? 0;

                    if (paymentType == "Porcentaje")
                    {
                        mechanicPayout += mechanicGroup.Sum(s => s.Price * (configValue / 100m));
                    }
                    else // PorDia
                    {
                        var uniqueDays = mechanicGroup
                            .Select(s => s.DateCompleted)
                            .Where(d => d != null)
                            .Distinct()
                            .Count();
                        mechanicPayout += uniqueDays * configValue;
                    }
                }

                decimal partsNetProfit = totalParts - partsCost;
                decimal laborNetProfit = totalServices - mechanicPayout;
                decimal netProfit = partsNetProfit + laborNetProfit;

                return new SalesSummaryDto
                {
                    TotalParts = totalParts,
                    TotalServices = totalServices,
                    TotalSales = totalParts + totalServices,
                    TotalDownPayments = totalDownPayments,
                    OrdersCount = ordersCount,
                    NetProfit = netProfit,
                    InStockPartsSales = inStockPartsSales,
                    OutOfStockPartsSales = outOfStockPartsSales,
                    ExternalQuotesSales = externalQuotesSales,
                    PartsCost = partsCost,
                    PartsNetProfit = partsNetProfit,
                    MechanicPayout = mechanicPayout,
                    LaborNetProfit = laborNetProfit
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
