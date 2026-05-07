using ApiTaller.Domain.Dtos.Inventory;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Inventory
{
    public interface IInventoryReceptionService
    {
        Task<IEnumerable<InventoryReceptionDto>> GetReceptionsAsync(CancellationToken cancellation);
        Task<bool> SaveReceptionAsync(InventoryReceptionDto value, CancellationToken cancellation);
    }
}
