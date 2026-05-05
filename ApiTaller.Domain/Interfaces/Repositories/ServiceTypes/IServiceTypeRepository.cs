using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.ServiceTypes
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<GetServiceTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServiceTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(ServiceType create, CancellationToken cancellation);
        Task<bool> UpdateAsync(ServiceType update, CancellationToken cancellation);
        Task<GetServiceTypeDto?> ValidateExist(string name, CancellationToken cancellation);
    }
}
