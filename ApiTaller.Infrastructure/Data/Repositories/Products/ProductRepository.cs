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
                var saved = await _context.SaveChangesAsync(cancellation) > 0;
                return saved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
            }
            return false;
        }

        public async Task<IEnumerable<GetProductDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductDto> result = [];
            try
            {
                result = await _context.Product
                    .Where(p => p.IsActive && p.ProductTypeIdNavigation.IsActive)
                    .Select(p => new GetProductDto
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        SalePrice = p.SalePrice,
                        Code = p.Code,
                        Reference = p.Reference,
                        Description = p.Description,
                        VehicleType = p.VehicleType,
                        ImageBase64 = p.ImageBase64,
                        IsCombo = p.IsCombo,
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
                        },
                        ComboItems = p.ComboItems.Where(ci => ci.IsActive).Select(ci => new ProductComboItemDto
                        {
                            Id = ci.Id,
                            ParentProductId = ci.ParentProductId,
                            ChildProductId = ci.ChildProductId,
                            ChildProductName = ci.ChildProduct.ProductName,
                            ChildProductCode = ci.ChildProduct.Code,
                            ChildProductPrice = ci.ChildProduct.Price,
                            ChildProductSalePrice = ci.ChildProduct.SalePrice,
                            Quantity = ci.Quantity
                        }).ToList()
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
                        VehicleType = p.VehicleType,
                        ImageBase64 = p.ImageBase64,
                        IsCombo = p.IsCombo,
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
                        },
                        ComboItems = p.ComboItems.Where(ci => ci.IsActive).Select(ci => new ProductComboItemDto
                        {
                            Id = ci.Id,
                            ParentProductId = ci.ParentProductId,
                            ChildProductId = ci.ChildProductId,
                            ChildProductName = ci.ChildProduct.ProductName,
                            ChildProductCode = ci.ChildProduct.Code,
                            ChildProductPrice = ci.ChildProduct.Price,
                            ChildProductSalePrice = ci.ChildProduct.SalePrice,
                            Quantity = ci.Quantity
                        }).ToList()
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
                        VehicleType = p.VehicleType,
                        ImageBase64 = p.ImageBase64,
                        IsCombo = p.IsCombo,
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
                        },
                        ComboItems = p.ComboItems.Where(ci => ci.IsActive).Select(ci => new ProductComboItemDto
                        {
                            Id = ci.Id,
                            ParentProductId = ci.ParentProductId,
                            ChildProductId = ci.ChildProductId,
                            ChildProductName = ci.ChildProduct.ProductName,
                            ChildProductCode = ci.ChildProduct.Code,
                            ChildProductPrice = ci.ChildProduct.Price,
                            ChildProductSalePrice = ci.ChildProduct.SalePrice,
                            Quantity = ci.Quantity
                        }).ToList()
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

                // Cargar entidad existente de la BD incluyendo sus ComboItems
                var existing = await _context.Product
                    .Include(p => p.ComboItems)
                    .FirstOrDefaultAsync(p => p.Id == update.Id, cancellation);

                if (existing == null) return false;

                existing.ProductName = update.ProductName;
                existing.Price = update.Price;
                existing.SalePrice = update.SalePrice;
                existing.Code = update.Code;
                existing.Reference = update.Reference;
                existing.Description = update.Description;
                existing.VehicleType = update.VehicleType;
                existing.ProducTypeId = update.ProducTypeId;
                existing.ImageBase64 = update.ImageBase64;
                existing.IsCombo = update.IsCombo;
                existing.IsActive = update.IsActive;
                existing.UpdatedAt = DateTime.Now;
                existing.ResponsibleUserId = update.ResponsibleUserId;

                // Sincronizar ComboItems
                if (update.IsCombo)
                {
                    // Remover items que ya no están
                    var newChildIds = update.ComboItems.Select(ci => ci.ChildProductId).ToHashSet();
                    var toRemove = existing.ComboItems.Where(ci => !newChildIds.Contains(ci.ChildProductId)).ToList();
                    foreach (var rem in toRemove)
                    {
                        _context.ProductComboItem.Remove(rem);
                    }

                    // Actualizar o agregar nuevos
                    foreach (var item in update.ComboItems)
                    {
                        var existItem = existing.ComboItems.FirstOrDefault(ci => ci.ChildProductId == item.ChildProductId);
                        if (existItem != null)
                        {
                            existItem.Quantity = item.Quantity;
                            existItem.IsActive = true;
                            existItem.UpdatedAt = DateTime.Now;
                        }
                        else
                        {
                            existing.ComboItems.Add(new ProductComboItem
                            {
                                ParentProductId = existing.Id,
                                ChildProductId = item.ChildProductId,
                                Quantity = item.Quantity,
                                WorkshopId = existing.WorkshopId,
                                IsActive = true,
                                CreatedAt = DateTime.Now,
                                ResponsibleUserId = update.ResponsibleUserId
                            });
                        }
                    }
                }
                else
                {
                    // Si ya no es combo, eliminar cualquier item que tuviera
                    if (existing.ComboItems.Any())
                    {
                        _context.ProductComboItem.RemoveRange(existing.ComboItems);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product with id {update.Id}");
                return false;
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
                        VehicleType = p.VehicleType,
                        ImageBase64 = p.ImageBase64,
                        IsCombo = p.IsCombo,
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
