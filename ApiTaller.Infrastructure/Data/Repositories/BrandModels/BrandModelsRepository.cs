using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Interfaces.Repositories.BrandModels;
using ApiTaller.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ApiTaller.Infrastructure.Data.Repositories.BrandModels
{
    public class BrandModelsRepository : IBrandModelsRepository
    {
        private readonly DataContext _Context;
        private readonly ICurrentUserService _CurrentUserService;
        private readonly ILogger<BrandModelsRepository> _Logger;
        public BrandModelsRepository(DataContext context, ICurrentUserService currentUserService, ILogger<BrandModelsRepository> logger)
        {
            _Context = context;
            _CurrentUserService = currentUserService;
            _Logger = logger;
        }
        public async Task<bool> CreateBrandModelAsync(Domain.Models.BrandModels brandModel, CancellationToken cancellationToken)
        {
            try
            {
                if (int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    brandModel.ResponsibleUserId = userId;
                }
                brandModel.CreatedAt = DateTime.Now;
                await _Context.AddAsync(brandModel, cancellationToken);

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error creating brand model");
            }
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelsDto> result = [];
            try
            {
                result = await _Context.BrandModels
                    .Where(bm => bm.IsActive)
                    .Select(bm => new GetBrandModelsDto
                    {
                        Id = bm.Id,
                        Models = bm.Models,
                        VehicleType = bm.VehicleType,
                        IsActive = bm.IsActive,
                        CreatedAt = bm.CreatedAt,
                        UpdatedAt = bm.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting all active brand models");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(string? vehicleType, CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelsDto> result = [];
            try
            {
                var query = _Context.BrandModels.Where(bm => bm.IsActive);
                if (!string.IsNullOrWhiteSpace(vehicleType))
                    query = query.Where(bm => bm.VehicleType == vehicleType);

                result = await query
                    .Select(bm => new GetBrandModelsDto
                    {
                        Id = bm.Id,
                        Models = bm.Models,
                        VehicleType = bm.VehicleType,
                        IsActive = bm.IsActive,
                        CreatedAt = bm.CreatedAt,
                        UpdatedAt = bm.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting active brand models filtered by vehicleType");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelsDto> result = [];
            try
            {
                result = await _Context.BrandModels
                    .Select(bm => new GetBrandModelsDto
                    {
                        Id = bm.Id,
                        Models = bm.Models,
                        VehicleType = bm.VehicleType,
                        IsActive = bm.IsActive,
                        CreatedAt = bm.CreatedAt,
                        UpdatedAt = bm.UpdatedAt
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting all brand models");
            }
            return result;
        }

        public async Task<GetBrandModelsDto?> GetBrandModelByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandModelsDto? result = null;
            try
            {
                result = await _Context.BrandModels
                    .Where(bm => bm.Id == id)
                    .Select(bm => new GetBrandModelsDto
                    {
                        Id = bm.Id,
                        Models = bm.Models,
                        VehicleType = bm.VehicleType,
                        IsActive = bm.IsActive,
                        CreatedAt = bm.CreatedAt,
                        UpdatedAt = bm.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error getting brand model by id: {id}");
            }
            return result;
        }
        public async Task<bool> UpdateBrandModelAsync(Domain.Models.BrandModels brandModel, CancellationToken cancellationToken)
        {
            try
            {
                if (int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    brandModel.ResponsibleUserId = userId;
                }
                brandModel.UpdatedAt = DateTime.Now;
                _Context.BrandModels.Update(brandModel);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error updating brand model with id: {brandModel.Id}");
            }
            return await _Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<GetBrandModelsDto?> ValidateExist(GetBrandModelsDto? brandModel, CancellationToken cancellationToken)
        {
            GetBrandModelsDto? brandDto = null;
            try
            {
                brandDto = await _Context.BrandModels
                    .Where(b => b.Models == brandModel.Models)
                    .Select(b => new GetBrandModelsDto
                    {
                        Id = b.Id,
                        Models = b.Models,
                        VehicleType = b.VehicleType,
                        IsActive = b.IsActive,
                        CreatedAt = b.CreatedAt,
                        UpdatedAt = b.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error validating existence of brand model with id: {brandModel?.Id}");
            }
            return brandDto;
        }
    }
}
