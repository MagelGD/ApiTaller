using ApiTaller.Domain.Dtos.ServiceTypes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.ServiceTypes
{
    public interface IServiceTypeService
    {
        Task<IEnumerable<GetServiceTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServiceTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetServiceTypeDto> CreateOrEditServiceType(GetServiceTypeDto serviceType, CancellationToken cancellationToken);
    }
}
