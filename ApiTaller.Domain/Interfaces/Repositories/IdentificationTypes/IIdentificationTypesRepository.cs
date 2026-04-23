using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.IdentificationTypes
{
    public interface IIdentificationTypesRepository
    {
        Task<IEnumerable<GetIdentificationTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<GetIdentificationTypeDto> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(IdentificationType identificationType, CancellationToken cancellation);
        Task<bool> UpdateAsync(IdentificationType identificationType, CancellationToken cancellation);
        Task<bool> ValidateExist(string name, CancellationToken cancellation);
        Task<IEnumerable<GetIdentificationTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetIdentificationTypeDto?> GetByNameAsync(string name, CancellationToken cancellation);
    }
}
