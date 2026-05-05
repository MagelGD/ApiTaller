using ApiTaller.Domain.Dtos.ServicePrices;
using ApiTaller.Domain.Interfaces.Repositories.ServicePrices;
using ApiTaller.Domain.Interfaces.Services.ServicePrices;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.ServicePrices
{
    public class ServicePriceByVersionService : IServicePriceByVersionService
    {
        private readonly IServicePriceByVersionRepository _repository;
        private readonly ILogger<ServicePriceByVersionService> _logger;

        public ServicePriceByVersionService(IServicePriceByVersionRepository repository, ILogger<ServicePriceByVersionService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<GetServicePriceByVersionDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllActiveAsync(cancellation);
        }

        public async Task<IEnumerable<GetServicePriceByVersionDto>> GetAllAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllAsync(cancellation);
        }

        public async Task<GetServicePriceByVersionDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            return await _repository.GetByIdAsync(id, cancellation);
        }

        public async Task<IEnumerable<GetServicePriceByVersionDto>> GetByVersionAsync(int versionId, CancellationToken cancellation)
        {
            return await _repository.GetByVersionAsync(versionId, cancellation);
        }

        public async Task<GetServicePriceByVersionDto> CreateOrEditServicePrice(GetServicePriceByVersionDto servicePrice, CancellationToken cancellationToken)
        {
            GetServicePriceByVersionDto result = new();
            try
            {
                ServicePriceByVersion saveData = new()
                {
                    Id = servicePrice.Id,
                    ServiceCatalogId = servicePrice.ServiceCatalogId,
                    BrandModelVersionId = servicePrice.BrandModelVersionId,
                    Price = servicePrice.Price,
                    EstimatedMinutes = servicePrice.EstimatedMinutes,
                    IsActive = servicePrice.IsActive,
                    CreatedAt = servicePrice.CreatedAt ?? DateTime.Now,
                    UpdatedAt = DateTime.UtcNow
                };

                bool exists = await ValidateExist(servicePrice.ServiceCatalogId, servicePrice.BrandModelVersionId, cancellationToken);

                if (saveData.Id == 0 && !exists)
                {
                    await _repository.CreateAsync(saveData, cancellationToken);
                    result = await _repository.ValidateExist(servicePrice.ServiceCatalogId, servicePrice.BrandModelVersionId, cancellationToken) ?? new GetServicePriceByVersionDto();
                }
                else if (saveData.Id != 0)
                {
                    await _repository.UpdateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetServicePriceByVersionDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing service price");
            }
            return result;
        }

        private async Task<bool> ValidateExist(int catalogId, int brandModelVersionId, CancellationToken cancellation)
        {
            var existing = await _repository.ValidateExist(catalogId, brandModelVersionId, cancellation);
            return existing != null;
        }
    }
}
