using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Interfaces.Repositories.ServiceTypes;
using ApiTaller.Domain.Interfaces.Services.ServiceTypes;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.ServiceTypes
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _repository;
        private readonly ILogger<ServiceTypeService> _logger;

        public ServiceTypeService(IServiceTypeRepository repository, ILogger<ServiceTypeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetServiceTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllActiveAsync(cancellation);
        }

        public async Task<IEnumerable<GetServiceTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllAsync(cancellation);
        }

        public async Task<GetServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await _repository.GetByIdAsync(id, cancellation);
        }

        public async Task<GetServiceTypeDto> CreateOrEditServiceType(GetServiceTypeDto serviceType, CancellationToken cancellationToken)
        {
            GetServiceTypeDto result = new();
            try
            {
                ServiceType saveData = new()
                {
                    Id = serviceType.Id,
                    Name = serviceType.Name,
                    IsActive = serviceType.IsActive,
                    CreatedAt = serviceType.CreatedAt ?? DateTime.Now,
                    UpdatedAt = DateTime.UtcNow
                };

                bool exists = await ValidateExist(serviceType.Name, cancellationToken);

                if (saveData.Id == 0 && !exists)
                {
                    await _repository.CreateAsync(saveData, cancellationToken);
                    result = await _repository.ValidateExist(serviceType.Name, cancellationToken) ?? new GetServiceTypeDto();
                }
                else if (saveData.Id != 0)
                {
                    await _repository.UpdateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetServiceTypeDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing service type");
            }
            return result;
        }

        private async Task<bool> ValidateExist(string name, CancellationToken cancellation)
        {
            var existing = await _repository.ValidateExist(name, cancellation);
            return existing != null;
        }
    }
}
