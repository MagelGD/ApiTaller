using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion
{
    public interface IBrandModelVersionRepository
    {
        Task<IEnumerable<GetBrandModelVersionDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(string? vehicleType, CancellationToken cancellation);
        Task<GetBrandModelVersionDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(Models.BrandModelVersion brandModelVersion, CancellationToken cancellation);
        Task<bool> UpdateAsync(Models.BrandModelVersion brandModelVersion, CancellationToken cancellation);
        Task<GetBrandModelVersionDto?> ValidateExist(GetBrandModelVersionDto dto, CancellationToken cancellation);
    }
}
