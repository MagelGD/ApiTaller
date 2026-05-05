using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Inventory
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync(CancellationToken cancellation);
        Task<InventoryDto> GetByProductIdAsync(int productId, CancellationToken cancellation);
        Task<bool> UpdateStockAsync(InventoryHistory movement, CancellationToken cancellation);
        Task<IEnumerable<InventoryHistoryDto>> GetHistoryByProductAsync(int productId, CancellationToken cancellation);
    }
}
