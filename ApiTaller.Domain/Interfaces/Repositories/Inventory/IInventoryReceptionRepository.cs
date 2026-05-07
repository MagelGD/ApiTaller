using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Inventory
{
    public interface IInventoryReceptionRepository
    {
        Task<IEnumerable<InventoryReception>> GetAllAsync(CancellationToken cancellation);
        Task<bool> CreateAsync(InventoryReception reception, CancellationToken cancellation);
    }
}
