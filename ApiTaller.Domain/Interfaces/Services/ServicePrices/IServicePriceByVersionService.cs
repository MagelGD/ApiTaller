using ApiTaller.Domain.Dtos.ServicePrices;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.ServicePrices
{
    public interface IServicePriceByVersionService
    {
        Task<IEnumerable<GetServicePriceByVersionDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServicePriceByVersionDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServicePriceByVersionDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetServicePriceByVersionDto> CreateOrEditServicePrice(GetServicePriceByVersionDto servicePrice, CancellationToken cancellationToken);
    }
}
