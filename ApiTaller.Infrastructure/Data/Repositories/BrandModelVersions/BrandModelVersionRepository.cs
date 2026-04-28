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
        public async Task<bool> CreateBrandModelVersionAsync(BrandModelVersion brandModelVersion, CancellationToken cancellationToken)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out var userId))
                {
                    brandModelVersion.ResponsibleUserId = userId;
                }
                brandModelVersion.CreatedAt = DateTime.Now;
                await _Context.AddAsync(brandModelVersion, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _Context.BrandModelVersion.Include(x=> x.Model).Include(x=> x.Brand).Where(x => x.IsActive).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt

                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        IsActive= x.Model.IsActive,
                        CreatedAt= x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<GetBrandModelVersionDto?> GetBrandModelVersionByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Where(x => x.Id == id).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<bool> UpdateBrandModelVersionAsync(BrandModelVersion brandModelVersion, CancellationToken cancellationToken)
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
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<GetBrandModelVersionDto?> ValidateExist(GetBrandModelVersionDto getBrandModelVersion, CancellationToken cancellationToken)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _Context.BrandModelVersion.Include(x => x.Model).Include(x => x.Brand).Where(x => x.Version == getBrandModelVersion.Version && x.Brand.Id == getBrandModelVersion.brandDto.Id && x.Model.Id == getBrandModelVersion.BrandModelsDto.Id).Select(x => new GetBrandModelVersionDto
                {
                    Id = x.Id,
                    Version = x.Version,
                    brandDto = new GetBrandDto()
                    {
                        Id = x.Brand.Id,
                        Name = x.Brand.Name,
                        IsActive = x.Brand.IsActive,
                        CreatedAt = x.Brand.CreatedAt
                    },
                    BrandModelsDto = new GetBrandModelsDto()
                    {
                        Id = x.Model.Id,
                        Models = x.Model.Models,
                        IsActive = x.Model.IsActive,
                        CreatedAt = x.Model.CreatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }
    }
}
