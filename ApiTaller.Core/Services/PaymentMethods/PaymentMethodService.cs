using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Interfaces.Repositories.PaymentMethods;
using ApiTaller.Domain.Interfaces.Services.PaymentMethods;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.PaymentMethods
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethosRepository _paymentMethodRepository;
        private readonly ILogger<PaymentMethodService> _logger;

        public PaymentMethodService(IPaymentMethosRepository paymentMethodRepository, ILogger<PaymentMethodService> logger)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _logger = logger;
        }
        public async Task<GetPaymentMethodDto> CreateOrEditPaymentMethod(GetPaymentMethodDto paymentMethod, CancellationToken cancellationToken)
        {
            GetPaymentMethodDto result = new();
            try
            {
                PaymentMethod saveData = new()
                {
                    Id = paymentMethod.Id,
                    Name = paymentMethod.Name,
                    Icon = paymentMethod.Icon,
                    IsActive = paymentMethod.IsActive,
                    CreatedAt = paymentMethod.CreatedAt ?? DateTime.Now
                };
                bool isExist = await ValidateExist(paymentMethod.Name, cancellationToken);
                if (saveData.Id == 0 && !isExist)
                {
                    await _paymentMethodRepository.CreateAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0)
                {
                    await _paymentMethodRepository.UpdateAsync(saveData, cancellationToken);
                }
                result = await _paymentMethodRepository.ValidateExist(saveData.Name, cancellationToken) ?? new GetPaymentMethodDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el método de pago");
            }
            return result;
        }

        public async Task<IEnumerable<GetPaymentMethodDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetPaymentMethodDto> result = [];
            try
            {
                result = await _paymentMethodRepository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los métodos de pago activos");
            }
            return result;
        }

        public async Task<IEnumerable<GetPaymentMethodDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetPaymentMethodDto> result = [];
            try
            {
                result = await _paymentMethodRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los métodos de pago");
            }
            return result;
        }

        public async Task<GetPaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetPaymentMethodDto? result = null;
            try
            {
                result = await _paymentMethodRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el método de pago con ID {id}");
            }
            return result;
        }

        private async Task<bool> ValidateExist(string type, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                GetPaymentMethodDto? existingPaymentMethod = await _paymentMethodRepository.ValidateExist(type, cancellation);
                result = existingPaymentMethod != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia del método de pago con tipo {type}");
            }
            return result;
        }
    }
}
