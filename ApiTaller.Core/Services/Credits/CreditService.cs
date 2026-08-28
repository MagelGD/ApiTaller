using ApiTaller.Domain.Dtos.Credits;
using ApiTaller.Domain.Interfaces.Repositories.Credits;
using ApiTaller.Domain.Interfaces.Services.Credits;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Credits
{
    public class CreditService : ICreditService
    {
        private readonly ICreditRepository _repository;
        private readonly ILogger<CreditService> _logger;

        public CreditService(ICreditRepository repository, ILogger<CreditService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerCreditSummaryDto>> GetCustomersWithCreditAsync(CancellationToken cancellation)
        {
            try
            {
                return await _repository.GetCustomersWithCreditAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en servicio al obtener clientes con saldo a crédito");
                return Array.Empty<CustomerCreditSummaryDto>();
            }
        }

        public async Task<CustomerCreditStatementDto?> GetCustomerStatementAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                return await _repository.GetCustomerStatementAsync(customerId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en servicio al obtener extracto de crédito del cliente {customerId}");
                return null;
            }
        }

        public async Task<bool> RegisterPaymentAsync(RegisterCreditPaymentDto dto, CancellationToken cancellation)
        {
            try
            {
                return await _repository.RegisterPaymentAsync(dto, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error en servicio al registrar abono para venta #{dto.SaleId}");
                throw;
            }
        }
    }
}
