using ApiTaller.Domain.Dtos.BrandModelVersion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.BrandModelVersion
{
    public interface IBrandModelVersionService
    {
        Task<IEnumerable<GetBrandModelVersionDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetBrandModelVersionDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetBrandModelVersionDto> CreateOrEditAsync(GetBrandModelVersionDto dto, CancellationToken cancellation);
    }
}
