using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.BrandModels
{
    public interface IBrandModelsRepository
    {
        Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(string? vehicleType, CancellationToken cancellationToken);
        Task<GetBrandModelsDto?> GetBrandModelByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateBrandModelAsync(Models.BrandModels brandModel, CancellationToken cancellationToken);
        Task<bool> UpdateBrandModelAsync(Models.BrandModels brandModel, CancellationToken cancellationToken);
        Task<GetBrandModelsDto?> ValidateExist(GetBrandModelsDto? brandModel, CancellationToken cancellationToken);
    }
}
