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
        /// <summary>CAR-1: Obtiene marcas activas filtradas por tipo de vehículo. Si vehicleType es null, retorna todas.</summary>
        Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(string? vehicleType, CancellationToken cancellationToken);
        Task<GetBrandDto?> GetBrandByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateBrandAsync(Brand brand, CancellationToken cancellationToken);
        Task<bool> UpdateBrandAsync(Brand brand, CancellationToken cancellationToken);
        Task<GetBrandDto?> ValidateExist(GetBrandDto? brand, CancellationToken cancellationToken);
    }
}
