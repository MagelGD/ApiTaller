using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Interfaces.Repositories.Brands;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Brands
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _Context;
        private readonly ICurrentUserService _CurrentUserService;
        private readonly ILogger<BrandRepository> _Logger;

        public BrandRepository(DataContext context, ICurrentUserService currentUserService, ILogger<BrandRepository> logger)
        {
            _Context = context;
            _CurrentUserService = currentUserService;
            _Logger = logger;
        }
        public async Task<bool> CreateBrandAsync(Brand brand, CancellationToken cancellationToken)
        {
            try
            {
                if(int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    brand.ResponsibleUserId = userId;
                }
                brand.CreatedAt = DateTime.Now;
                await _Context.AddAsync(brand, cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error creating brand");
            }
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandDto> brands = [];
            try
            {
                brands = await _Context.Brand
                    .Where(b => b.IsActive == true)
                    .Select(b => new GetBrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        IsActive = b.IsActive,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting active brands");
            }
            return brands;
        }

        public async Task<IEnumerable<GetBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandDto> brands = [];
            try
            {
                brands = await _Context.Brand
                    .Select(b => new GetBrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        IsActive = b.IsActive,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting all brands");
            }
            return brands;
        }

        public async Task<GetBrandDto?> GetBrandByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandDto? brand = null;
            try
            {
                brand = await _Context.Brand
                    .Where(b => b.Id == id)
                    .Select(b => new GetBrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        IsActive = b.IsActive,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting brand by id");
            }
            return brand;
        }

        public async Task<bool> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken)
        {
            try
            {
                if(int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    brand.ResponsibleUserId = userId;
                }
                brand.UpdatedAt = DateTime.Now;
                _Context.Update(brand);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error updating brand");
            }
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<GetBrandDto?> ValidateExist(GetBrandDto? brand, CancellationToken cancellationToken)
        {
            GetBrandDto? brandDto = null;
            try
            {
                brandDto = await _Context.Brand
                    .Where(b => b.Name.Equals(brand.Name, StringComparison.CurrentCultureIgnoreCase))
                    .Select(b => new GetBrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                        IsActive = b.IsActive,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error validating if brand exist");
            }
            return brandDto;
        }
    }
}
