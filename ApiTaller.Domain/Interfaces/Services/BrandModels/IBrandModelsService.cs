using ApiTaller.Domain.Dtos.BrandModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.BrandModels
{
    public interface IBrandModelsService
    {
        Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(CancellationToken cancellationToken);
        Task<GetBrandModelsDto?> GetBrandModelByIdAsync(int id, CancellationToken cancellationToken);
        Task<GetBrandModelsDto> CreateOrEditBrandModel(GetBrandModelsDto brandModelDto, CancellationToken cancellationToken);
    }
}
