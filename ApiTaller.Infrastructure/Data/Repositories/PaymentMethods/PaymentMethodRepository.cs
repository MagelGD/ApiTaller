using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Interfaces.Repositories.PaymentMethods;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiTaller.Infrastructure.Data.Repositories.PaymentMethods
{
    public class PaymentMethodRepository : IPaymentMethosRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<PaymentMethodRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public PaymentMethodRepository(DataContext context, ILogger<PaymentMethodRepository> logger, ICurrentUserService currentUserService)
        {
            _Context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public async Task<bool> CreateAsync(PaymentMethod create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _Context.PaymentMethod.AddAsync(create, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment method");
            }
            return await _Context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<IEnumerable<GetPaymentMethodDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetPaymentMethodDto> result = [];
            try
            {
                result = await _Context.PaymentMethod
                    .Where(pm => pm.IsActive)
                    .Select(pm => new GetPaymentMethodDto
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        Icon = pm.Icon,
                        CreatedAt = pm.CreatedAt,
                        UpdatedAt = pm.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active payment methods");
            }
            return result;
        }

        public async Task<IEnumerable<GetPaymentMethodDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetPaymentMethodDto> result = [];
            try
            {
                result = await _Context.PaymentMethod
                    .Select(pm => new GetPaymentMethodDto
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        Icon = pm.Icon,
                        CreatedAt = pm.CreatedAt,
                        UpdatedAt = pm.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active payment methods");
            }
            return result;
        }

        public async Task<GetPaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetPaymentMethodDto? result = null;
            try
            {
                result = await _Context.PaymentMethod
                    .Where(pm => pm.Id == id)
                    .Select(pm => new GetPaymentMethodDto
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        Icon = pm.Icon,
                        CreatedAt = pm.CreatedAt,
                        UpdatedAt = pm.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting payment method by id");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(PaymentMethod update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _Context.PaymentMethod.Update(update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment method");
            }
            return await _Context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<GetPaymentMethodDto?> ValidateExist(string type, CancellationToken cancellation)
        {
            GetPaymentMethodDto? result = null;
            try
            {
                result = await _Context.PaymentMethod
                    .Where(pm => pm.Name.ToLower() == type.ToLower())
                    .Select(pm => new GetPaymentMethodDto
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        Icon = pm.Icon,
                        CreatedAt = pm.CreatedAt,
                        UpdatedAt = pm.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating payment method existence");
            }
            return result;
        }
    }
}
