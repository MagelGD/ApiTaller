using ApiTaller.Domain.Dtos.ServicePrices;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.ServicePrices
{
    public interface IServicePriceByVersionRepository
    {
        Task<IEnumerable<GetServicePriceByVersionDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServicePriceByVersionDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServicePriceByVersionDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(ServicePriceByVersion create, CancellationToken cancellation);
        Task<bool> UpdateAsync(ServicePriceByVersion update, CancellationToken cancellation);
        Task<GetServicePriceByVersionDto?> ValidateExist(int serviceCatalogId, int brandModelVersionId, CancellationToken cancellation);
        Task<IEnumerable<GetServicePriceByVersionDto>> GetByVersionAsync(int versionId, CancellationToken cancellation);
    }
}
