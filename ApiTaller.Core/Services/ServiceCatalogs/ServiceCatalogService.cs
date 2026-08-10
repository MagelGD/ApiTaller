using ApiTaller.Domain.Dtos.ServiceCatalogs;
using ApiTaller.Domain.Interfaces.Repositories.ServiceCatalogs;
using ApiTaller.Domain.Interfaces.Services.ServiceCatalogs;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.ServiceCatalogs
{
    public class ServiceCatalogService : IServiceCatalogService
    {
        private readonly IServiceCatalogRepository _repository;
        private readonly ILogger<ServiceCatalogService> _logger;

        public ServiceCatalogService(IServiceCatalogRepository repository, ILogger<ServiceCatalogService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetServiceCatalogDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllActiveAsync(cancellation);
        }

        public async Task<IEnumerable<GetServiceCatalogDto>> GetAllAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllAsync(cancellation);
        }

        public async Task<GetServiceCatalogDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await _repository.GetByIdAsync(id, cancellation);
        }

        public async Task<GetServiceCatalogDto> CreateOrEditServiceCatalog(GetServiceCatalogDto serviceCatalog, CancellationToken cancellationToken)
        {
            GetServiceCatalogDto result = new();
            try
            {
                ServiceCatalog saveData = new()
                {
                    Id = serviceCatalog.Id,
                    ServiceTypeId = serviceCatalog.ServiceTypeId,
                    Name = serviceCatalog.Name,
                    Description = serviceCatalog.Description,
                    DefaultPrice = serviceCatalog.DefaultPrice,
                    DefaultMinutes = serviceCatalog.DefaultMinutes,
                    TimeUnit = serviceCatalog.TimeUnit,
                    IsActive = serviceCatalog.IsActive,
                    CreatedAt = serviceCatalog.CreatedAt ?? DateTime.Now,
                    UpdatedAt = DateTime.UtcNow
                };

                bool exists = await ValidateExist(serviceCatalog.Name, cancellationToken);

                if (saveData.Id == 0 && !exists)
                {
                    await _repository.CreateAsync(saveData, cancellationToken);
                    result = await _repository.ValidateExist(serviceCatalog.Name, cancellationToken) ?? new GetServiceCatalogDto();
                }
                else if (saveData.Id != 0)
                {
                    await _repository.UpdateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetServiceCatalogDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing service catalog");
            }
            return result;
        }

        private async Task<bool> ValidateExist(string name, CancellationToken cancellation)
        {
            Domain.Models.ServiceCatalog? existing = await _repository.ValidateExist(name, cancellation);
            return existing != null;
        }
    }
}
