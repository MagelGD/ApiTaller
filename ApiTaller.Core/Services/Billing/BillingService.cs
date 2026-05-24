using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Repositories.Billing;
using ApiTaller.Domain.Interfaces.Services.Billing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Billing
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;
        private readonly ILogger<BillingService> _logger;

        public BillingService(IBillingRepository billingRepository, ILogger<BillingService> logger)
        {
            _billingRepository = billingRepository;
            _logger = logger;
        }

        public async Task<bool> SaveSaleAsync(SaleDto saleDto, CancellationToken cancellation)
        {
            try
            {
                return await _billingRepository.SaveSaleAsync(saleDto, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la factura para la orden {WorkOrderId}", saleDto.WorkOrderId);
                throw;
            }
        }

        public async Task<SaleDto> GetByWorkOrderAsync(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return await _billingRepository.GetByWorkOrderAsync(workOrderId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la factura para la orden {WorkOrderId}", workOrderId);
                throw;
            }
        }
    }
}
