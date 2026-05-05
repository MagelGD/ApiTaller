using ApiTaller.Domain.Dtos.ServiceCatalogs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.ServiceCatalogs
{
    public interface IServiceCatalogService
    {
        Task<IEnumerable<GetServiceCatalogDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServiceCatalogDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServiceCatalogDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetServiceCatalogDto> CreateOrEditServiceCatalog(GetServiceCatalogDto serviceCatalog, CancellationToken cancellationToken);
    }
}
