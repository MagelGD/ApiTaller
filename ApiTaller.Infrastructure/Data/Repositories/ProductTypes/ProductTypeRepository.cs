using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Interfaces.Repositories.ProductTypes;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.ProductTypes
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ProductTypeRepository> _logger;

        public ProductTypeRepository(DataContext context, ICurrentUserService currentUserService, ILogger<ProductTypeRepository> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(ProductType create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.ProductType.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product type");
            }
            return false;
        }

        public async Task<IEnumerable<GetProductTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductTypeDto> result = [];
            try
            {
                result = await _context.ProductType
                    .Where(pt => pt.IsActive)
                    .Select(pt => new GetProductTypeDto
                    {
                        Id = pt.Id,
                        Type = pt.Type,
                        IsActive = pt.IsActive,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        ResponsibleUser = pt.ResponsibleUserIdNavigation.Username
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active product types");
            }
            return result;
        }

        public async Task<IEnumerable<GetProductTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetProductTypeDto> productTypes = [];
            try
            {
                productTypes = await _context.ProductType
                    .Select(pt => new GetProductTypeDto
                    {
                        Id = pt.Id,
                        Type = pt.Type,
                        IsActive = pt.IsActive,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        ResponsibleUser = pt.ResponsibleUserIdNavigation.Username
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product types");
            }
            return productTypes;
        }

        public async Task<GetProductTypeDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetProductTypeDto? productType = null;
            try
            {
                productType = await _context.ProductType
                    .Where(pt => pt.Id == id)
                    .Select(pt => new GetProductTypeDto
                    {
                        Id = pt.Id,
                        Type = pt.Type,
                        IsActive = pt.IsActive,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        ResponsibleUser = pt.ResponsibleUserIdNavigation.Username
                    }).FirstOrDefaultAsync(cancellation) ?? new(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product type with id {id}");
            }
            return productType;
        }

        public async Task<bool> UpdateAsync(ProductType update, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.ProductType.Update(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product type with id {update.Id}");
            }
            return false;
        }

        public async Task<GetProductTypeDto?> ValidateExist(string type, CancellationToken cancellation)
        {
            GetProductTypeDto? result = null;
            try
            {
                result = await _context.ProductType
                    .Where(pt => pt.Type == type)
                    .Select(pt => new GetProductTypeDto
                    {
                        Id = pt.Id,
                        Type = pt.Type,
                        IsActive = pt.IsActive,
                        CreatedAt = pt.CreatedAt,
                        UpdatedAt = pt.UpdatedAt,
                        ResponsibleUser = pt.ResponsibleUserIdNavigation.Username
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of product type with type {type}");
            }
            return result;
        }
    }
}
