using ApiTaller.Domain.Dtos.ServiceCatalogs;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.ServiceCatalogs
{
    public interface IServiceCatalogRepository
    {
        Task<IEnumerable<GetServiceCatalogDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetServiceCatalogDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetServiceCatalogDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(ServiceCatalog create, CancellationToken cancellation);
        Task<bool> UpdateAsync(ServiceCatalog update, CancellationToken cancellation);
        Task<GetServiceCatalogDto?> ValidateExist(string name, CancellationToken cancellation);
    }
}
