using ApiTaller.Domain.Dtos.IdentificationTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.IdentificationTypes
{
    public interface IIdentificationTypesService
    {
        Task<IEnumerable<GetIdentificationType>> GetAllActiveAsync(CancellationToken cancellation);
        Task<IEnumerable<GetIdentificationType>> GetAllAsync(CancellationToken cancellation);
        Task<bool> CreateOrEditIdentificationType(GetIdentificationType createDto, CancellationToken cancellation);
        Task<GetIdentificationType?> GetByIdAsync(int id, CancellationToken cancellation);
    }
}