using ApiTaller.Domain.Dtos.BrandModelVersion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.BrandModelVersion
{
    public interface IBrandModelVersion
    {
        Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionAsync(CancellationToken cancellationToken);
        Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionActiveAsync(CancellationToken cancellationToken);
        Task<GetBrandModelVersionDto?> GetBrandModelVersionByIdAsync(int id, CancellationToken cancellationToken);
        Task<GetBrandModelVersionDto> CreateOrEditBrandModelVersionAsync(GetBrandModelVersionDto getBrandModelVersionDto, CancellationToken cancellationToken);
    }
}
