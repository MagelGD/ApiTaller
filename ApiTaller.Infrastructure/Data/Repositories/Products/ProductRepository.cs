using ApiTaller.Domain.Dtos.Product;
using ApiTaller.Domain.Interfaces.Repositories.Products;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApiTaller.Domain.Dtos.ProductType;

namespace ApiTaller.Infrastructure.Data.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ProductRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public ProductRepository(DataContext context, ILogger<ProductRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public async Task<bool> CreateAsync(Product create, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.Product.AddAsync(create, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
            }
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductDto> result = [];
            try
            {
                result = await _context.Product
                    .Where(p => p.IsActive)
                    .Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        Code = p.Code,
                        Reference = p.Reference,
                        Description = p.Description,
                        IsActive = p.IsActive,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ProductType = new GetProductTypeDto
                        {
                            Id = p.ProductTypeIdNavigation.Id,
                            Type = p.ProductTypeIdNavigation.Type,
                            IsActive = p.ProductTypeIdNavigation.IsActive,
                            CreatedAt = p.ProductTypeIdNavigation.CreatedAt,
                            UpdatedAt = p.ProductTypeIdNavigation.UpdatedAt
                        }
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active products");
            }
            return result;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductDto> result = [];
            try
            {
                result = await _context.Product
                    .Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        Code = p.Code,
                        Reference = p.Reference,
                        Description = p.Description,
                        IsActive = p.IsActive,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ProductType = new GetProductTypeDto
                        {
                            Id = p.ProductTypeIdNavigation.Id,
                            Type = p.ProductTypeIdNavigation.Type,
                            IsActive = p.ProductTypeIdNavigation.IsActive,
                            CreatedAt = p.ProductTypeIdNavigation.CreatedAt,
                            UpdatedAt = p.ProductTypeIdNavigation.UpdatedAt
                        }
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
            }
            return result;
        }

        public async Task<GetProductDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetProductDto? result = null;
            try
            {
                result = await _context.Product
                    .Where(p => p.Id == id)
                    .Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        Code = p.Code,
                        Reference = p.Reference,
                        Description = p.Description,
                        IsActive = p.IsActive,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ProductType = new GetProductTypeDto
                        {
                            Id = p.ProductTypeIdNavigation.Id,
                            Type = p.ProductTypeIdNavigation.Type,
                            IsActive = p.ProductTypeIdNavigation.IsActive,
                            CreatedAt = p.ProductTypeIdNavigation.CreatedAt,
                            UpdatedAt = p.ProductTypeIdNavigation.UpdatedAt
                        }
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting product with id {id}");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(Product update, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.Product.Update(update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with id {update.Id}");
            }
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<GetProductDto?> ValidateExist(string name, int idProductType, CancellationToken cancellation)
        {
            GetProductDto? result = null;
            try
            {
                result = await _context.Product
                    .Where(p => p.ProductName == name && p.ProducTypeId == idProductType)
                    .Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        Code = p.Code,
                        Reference = p.Reference,
                        Description = p.Description,
                        IsActive = p.IsActive,
                        CreatedAt = p.CreatedAt,
                        UpdatedAt = p.UpdatedAt,
                        ProductType = new GetProductTypeDto
                        {
                            Id = p.ProductTypeIdNavigation.Id,
                            Type = p.ProductTypeIdNavigation.Type,
                            IsActive = p.ProductTypeIdNavigation.IsActive,
                            CreatedAt = p.ProductTypeIdNavigation.CreatedAt,
                            UpdatedAt = p.ProductTypeIdNavigation.UpdatedAt
                        }
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of product with name {name} and product type id {idProductType}");
            }
            return result;
        }
    }
}
