using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion
{
    public interface IBrandModelVersionRepository
    {
        Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionActiveAsync(CancellationToken cancellationToken);
        Task<GetBrandModelVersionDto?> GetBrandModelVersionByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> CreateBrandModelVersionAsync(Models.BrandModelVersion brandModelVersion, CancellationToken cancellationToken);
        Task<bool> UpdateBrandModelVersionAsync(Models.BrandModelVersion brandModelVersion, CancellationToken cancellationToken);
        Task<GetBrandModelVersionDto?> ValidateExist(GetBrandModelVersionDto getBrandModelVersion ,CancellationToken cancellationToken);
    }
}
