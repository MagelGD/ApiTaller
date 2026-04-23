using ApiTaller.Domain.Dtos.IdentificationTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.IdentificationTypes
{
    public interface IIdentificationTypesService
    {
        Task<IEnumerable<GetIdentificationTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<IEnumerable<GetIdentificationTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<GetIdentificationTypeDto> CreateOrEditIdentificationType(GetIdentificationTypeDto createDto, CancellationToken cancellation);
        Task<GetIdentificationTypeDto?> GetByIdAsync(int id, CancellationToken cancellation);
    }
}