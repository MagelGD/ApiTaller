using ApiTaller.Domain.Dtos.Inventory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Inventory
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync(CancellationToken cancellation);
        Task<InventoryDto> GetByProductIdAsync(int productId, CancellationToken cancellation);
        Task<bool> AddStockAsync(InventoryHistoryDto movement, CancellationToken cancellation);
        Task<bool> RemoveStockAsync(InventoryHistoryDto movement, CancellationToken cancellation);
        Task<bool> AdjustStockAsync(InventoryHistoryDto movement, CancellationToken cancellation);
        Task<IEnumerable<InventoryHistoryDto>> GetHistoryAsync(int productId, CancellationToken cancellation);
    }
}
