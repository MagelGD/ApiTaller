using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.BrandModelVersions
{
    public class BrandModelVersionRepository : IBrandModelVersionRepository
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly DataContext _Context;
        private readonly ILogger<BrandModelVersionRepository> _logger;
        public BrandModelVersionRepository(ICurrentUserService currentUserService, DataContext dataContext, ILogger<BrandModelVersionRepository> logger)
        {
            _Context = dataContext;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(BrandModelVersion brandModelVersion, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out var userId))
                {
                    brandModelVersion.ResponsibleUserId = userId;
                }
                brandModelVersion.CreatedAt = DateTime.Now;
                await _Context.AddAsync(brandModelVersion, cancellation);
                return await _Context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _Context.BrandModelVersion.Include(x=> x.Model).Include(x=> x.Brand).Where(x => x.IsActive).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    VehicleType = x.VehicleType,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        VehicleType = x.Brand.VehicleType,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        VehicleType = x.Model.VehicleType,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active versions");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(string? vehicleType, CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                var query = _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Where(x => x.IsActive);
                if (!string.IsNullOrWhiteSpace(vehicleType))
                    query = query.Where(x => x.VehicleType == vehicleType);

                result = await query.Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    VehicleType = x.VehicleType,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        VehicleType = x.Brand.VehicleType,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        VehicleType = x.Model.VehicleType,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active versions filtered by vehicleType");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    VehicleType = x.VehicleType,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        VehicleType = x.Brand.VehicleType,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        VehicleType = x.Model.VehicleType,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<GetBrandModelVersionDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Where(x => x.Id == id).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    VehicleType = x.VehicleType,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        VehicleType = x.Brand.VehicleType,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        VehicleType = x.Model.VehicleType,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(BrandModelVersion brandModelVersion, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_currentUserService.UserId, out var userId))
                {
                    brandModelVersion.ResponsibleUserId = userId;
                }
                brandModelVersion.UpdatedAt = DateTime.Now;
                _Context.Update(brandModelVersion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return await _Context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<GetBrandModelVersionDto?> ValidateExist(GetBrandModelVersionDto dto, CancellationToken cancellation)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Where(x => x.Version == dto.Version && x.Brand.Id == dto.brandDto.Id && x.Model.Id == dto.BrandModelsDto.Id).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    VehicleType = x.VehicleType,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        VehicleType = x.Brand.VehicleType,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        VehicleType = x.Model.VehicleType,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }
    }
}
