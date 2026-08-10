using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Repositories.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync(CancellationToken cancellation)
        {
            return await _repository.GetAllAsync(cancellation);
        }

        public async Task<InventoryDto> GetByProductIdAsync(int productId, CancellationToken cancellation)
        {
            return await _repository.GetByProductIdAsync(productId, cancellation);
        }

        public async Task<bool> AddStockAsync(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            Domain.Models.InventoryMovement model = MapToModel(movement, "Entrada");
            return await _repository.UpdateStockAsync(model, cancellation);
        }

        public async Task<bool> RemoveStockAsync(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            Domain.Models.InventoryMovement model = MapToModel(movement, "Salida");
            return await _repository.UpdateStockAsync(model, cancellation);
        }

        public async Task<bool> AdjustStockAsync(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            Domain.Models.InventoryMovement model = MapToModel(movement, "Ajuste");
            return await _repository.UpdateStockAsync(model, cancellation);
        }

        public async Task<IEnumerable<InventoryHistoryDto>> GetHistoryAsync(int productId, CancellationToken cancellation)
        {
            return await _repository.GetHistoryByProductAsync(productId, cancellation);
        }

        private InventoryHistory MapToModel(InventoryHistoryDto dto, string type)
        {
            return new InventoryHistory
            {
                ProductId = dto.ProductId,
                MovementType = type,
                Quantity = dto.Quantity,
                ReferenceId = dto.ReferenceId,
                Observations = dto.Observations
            };
        }
    }
}
