using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Interfaces.Repositories.ProductTypes;
using ApiTaller.Domain.Interfaces.Services.ProductTypes;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.ProductTypes
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _repository;
        private readonly ILogger<ProductTypeService> _logger;

        public ProductTypeService(IProductTypeRepository repository, ILogger<ProductTypeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<GetProductTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductTypeDto> result = [];
            try
            {
                result = await _repository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active product types");
            }
            return result;
        }

        public async Task<IEnumerable<GetProductTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductTypeDto> result = [];
            try
            {
                result = await _repository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all product types");
            }
            return result;
        }

        public async Task<GetProductTypeDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetProductTypeDto? result = null;
            try
            {
                result = await _repository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product type with ID {id}");
            }
            return result;
        }
        public async Task<GetProductTypeDto> CreateOrEditProductType(GetProductTypeDto productType, CancellationToken cancellationToken)
        {
            GetProductTypeDto result = new();
            try
            {
                ProductType saveData = new()
                {
                    Id = productType.Id,
                    Type = productType.Type,
                    IsActive = productType.IsActive,
                    CreatedAt = productType.CreatedAt ?? DateTime.Now,
                    UpdatedAt = DateTime.UtcNow
                };

                if (saveData.Id != 0)
                {
                    // Editar o cambiar estado: siempre actualizar cuando hay Id
                    _ = await _repository.UpdateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetProductTypeDto();
                }
                else
                {
                    // Crear nuevo: validar que no exista otro con el mismo nombre
                    bool exists = await ValidateExist(productType.Type, cancellationToken);
                    if (!exists)
                    {
                        _ = await _repository.CreateAsync(saveData, cancellationToken);
                        result = await _repository.ValidateExist(productType.Type, cancellationToken) ?? new GetProductTypeDto();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing product type");
            }
            return result;
        }
        private async Task<bool> ValidateExist(string type, CancellationToken cancellation)
        {
            bool exists = false;
            try
            {
                GetProductTypeDto? existingProductType = await _repository.ValidateExist(type, cancellation);
                exists = existingProductType != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of product type '{type}'");
            }
            return exists;
        }
    }
}
