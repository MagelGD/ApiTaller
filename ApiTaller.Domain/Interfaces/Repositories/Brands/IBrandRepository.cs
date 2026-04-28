using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Brands
{
    public interface IBrandRepository
    {
        Task<IEnumerable<GetBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(CancellationToken cancellationToken);
        Task<GetBrandDto?> GetBrandByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateBrandAsync(Brand brand, CancellationToken cancellationToken);
        Task<bool> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken);
        Task<GetBrandDto?> ValidateExist(GetBrandDto? brand, CancellationToken cancellationToken);
    }
}
