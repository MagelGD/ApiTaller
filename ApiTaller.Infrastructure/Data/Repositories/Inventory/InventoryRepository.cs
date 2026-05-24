using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Repositories.Inventory;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Inventory
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;

        public InventoryRepository(DataContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync(CancellationToken cancellation)
        {
            var query = from p in _context.Product
                        join i in _context.Inventory on p.Id equals i.ProductId into inv
                        from subInv in inv.DefaultIfEmpty()
                        where p.IsActive
                        select new InventoryDto
                        {
                            Id = subInv != null ? subInv.Id : 0,
                            ProductId = p.Id,
                            ProductName = p.ProductName,
                            StockQuantity = subInv != null ? subInv.StockQuantity : 0,
                            MinStock = subInv != null ? subInv.MinStock : 0,
                            LastUpdate = subInv != null ? subInv.LastUpdate : p.CreatedAt,
                            IsActive = p.IsActive,
                            CreatedAt = p.CreatedAt,
                            UpdatedAt = p.UpdatedAt
                        };
            return await query.ToListAsync(cancellation);
        }

        public async Task<InventoryDto> GetByProductIdAsync(int productId, CancellationToken cancellation)
        {
            var query = from p in _context.Product
                        join i in _context.Inventory on p.Id equals i.ProductId into inv
                        from subInv in inv.DefaultIfEmpty()
                        where p.Id == productId
                        select new InventoryDto
                        {
                            Id = subInv != null ? subInv.Id : 0,
                            ProductId = p.Id,
                            ProductName = p.ProductName,
                            StockQuantity = subInv != null ? subInv.StockQuantity : 0,
                            MinStock = subInv != null ? subInv.MinStock : 0,
                            LastUpdate = subInv != null ? subInv.LastUpdate : p.CreatedAt,
                            IsActive = p.IsActive,
                            CreatedAt = p.CreatedAt,
                            UpdatedAt = p.UpdatedAt
                        };
            return await query.FirstOrDefaultAsync(cancellation);
        }

        public async Task<bool> UpdateStockAsync(InventoryHistory movement, CancellationToken cancellation)
        {
            try
            {
                var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == movement.ProductId, cancellation);
                
                if (inventory == null)
                {
                    // Si no existe el registro de inventario para este producto, crearlo
                    inventory = new Domain.Models.Inventory
                    {
                        ProductId = movement.ProductId,
                        StockQuantity = 0,
                        MinStock = 0,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    if (int.TryParse(_currentUserService.UserId, out int uid)) inventory.ResponsibleUserId = uid;
                    await _context.Inventory.AddAsync(inventory, cancellation);
                }

                // Actualizar cantidad según el tipo de movimiento
                if (movement.MovementType == "Entrada")
                    inventory.StockQuantity += movement.Quantity;
                else if (movement.MovementType == "Salida")
                    inventory.StockQuantity -= movement.Quantity;
                else if (movement.MovementType == "Ajuste")
                    inventory.StockQuantity = movement.Quantity; // En ajuste se setea el valor real

                inventory.LastUpdate = DateTime.Now;
                inventory.UpdatedAt = DateTime.Now;

                // Guardar historial
                movement.CreatedAt = DateTime.Now;
                movement.IsActive = true;
                if (int.TryParse(_currentUserService.UserId, out int userId)) movement.ResponsibleUserId = userId;
                await _context.InventoryHistory.AddAsync(movement, cancellation);

                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<InventoryHistoryDto>> GetHistoryByProductAsync(int productId, CancellationToken cancellation)
        {
            return await _context.InventoryHistory
                .Include(h => h.ProductNavigation)
                .Include(h => h.ResponsibleUserIdNavigation)
                .Include(h => h.SupplierNavigation)
                .Where(h => h.ProductId == productId)
                .OrderByDescending(h => h.CreatedAt)
                .Select(h => new InventoryHistoryDto
                {
                    Id = h.Id,
                    ProductId = h.ProductId,
                    ProductName = h.ProductNavigation.ProductName,
                    MovementType = h.MovementType,
                    Quantity = h.Quantity,
                    ReferenceId = h.ReferenceId,
                    SupplierId = h.SupplierId,
                    SupplierName = h.SupplierNavigation != null ? h.SupplierNavigation.BusinessName : null,
                    Observations = h.Observations,
                    CreatedAt = h.CreatedAt,
                    ResponsibleUserName = h.ResponsibleUserIdNavigation.FullName
                }).ToListAsync(cancellation);
        }
    }
}
