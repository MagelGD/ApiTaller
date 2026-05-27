using ApiTaller.Domain.Dtos.Brand;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Brands
{
    public interface IBrandService
    {
        Task<IEnumerable<GetBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(string? vehicleType, CancellationToken cancellationToken);
        Task<GetBrandDto?> GetBrandByIdAsync(int id, CancellationToken cancellationToken);
        Task<GetBrandDto> CreateOrEditBrand(GetBrandDto brandDto, CancellationToken cancellationToken);
    }
}
