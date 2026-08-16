using ApiTaller.Domain.Dtos.Product;
using ApiTaller.Domain.Interfaces.Repositories.Products;
using ApiTaller.Domain.Interfaces.Services.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetProductDto> CreateOrEditProductType(GetProductDto product, CancellationToken cancellationToken)
        {
            GetProductDto result = new();
            try
            {
                ApiTaller.Domain.Models.Product saveData = new()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    SalePrice = product.SalePrice,
                    Code = product.Code,
                    Reference = product.Reference,
                    Description = product.Description,
                    VehicleType = string.IsNullOrEmpty(product.VehicleType) ? "both" : product.VehicleType,
                    ProducTypeId = product.ProductType?.Id ?? 0,
                    IsActive = product.IsActive,
                    CreatedAt = product.Id == 0 ? DateTime.Now : product.CreatedAt,
                    UpdatedAt = DateTime.Now
                };

                if (saveData.Id == 0)
                {
                     await _repository.CreateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetProductDto();
                }
                else
                {
                    await _repository.UpdateAsync(saveData, cancellationToken);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellationToken) ?? new GetProductDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el producto");
            }
            return result;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductDto> products = [];
            try
            {
                products = await _repository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos activos");
            }
            return products;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductDto> products = [];
            try
            {
                products = await _repository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
            }
            return products;
        }

        public async Task<GetProductDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetProductDto? product = null;
            try
            {
                product = await _repository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el producto con ID {id}");
            }
            return product;
        }
    }
}
