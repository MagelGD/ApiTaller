using ApiTaller.Domain.Interfaces.Repositories.Inventory;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Inventory
{
    public class InventoryReceptionRepository : IInventoryReceptionRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<InventoryReceptionRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public InventoryReceptionRepository(DataContext context, ILogger<InventoryReceptionRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<InventoryReception>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.InventoryReception
                    .Include(r => r.SupplierNavigation)
                    .Include(r => r.Details)
                        .ThenInclude(d => d.ProductNavigation)
                    .OrderByDescending(r => r.ReceptionDate)
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las recepciones de inventario");
                return new List<InventoryReception>();
            }
        }

        public async Task<bool> CreateAsync(InventoryReception reception, CancellationToken cancellation)
        {
            try
            {
                int userId = 0;
                if (int.TryParse(_currentUserService.UserId, out userId))
                {
                    reception.ResponsibleUserId = userId;
                }

                reception.ReceptionDate = DateTime.Now;
                reception.CreatedAt = DateTime.Now;
                reception.IsActive = true;
                reception.TotalAmount = reception.Details.Sum(d => d.Quantity * d.UnitCost);

                await _context.InventoryReception.AddAsync(reception, cancellation);

                // Procesar cada detalle y actualizar inventario
                foreach (var detail in reception.Details)
                {

                    var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId, cancellation);
                    if (inventory == null)
                    {
                        inventory = new Domain.Models.Inventory
                        {
                            ProductId = detail.ProductId,
                            StockQuantity = 0,
                            MinStock = 0,
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            ResponsibleUserId = userId != 0 ? userId : null
                        };
                        await _context.Inventory.AddAsync(inventory, cancellation);
                    }

                    inventory.StockQuantity += detail.Quantity;
                    inventory.LastUpdate = DateTime.Now;
                    inventory.UpdatedAt = DateTime.Now;

                    // Actualizar Maestro de Productos
                    var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == detail.ProductId, cancellation);
                    if (product != null)
                    {
                        product.Price = detail.UnitCost;
                        product.SalePrice = detail.SalePrice;
                        product.UpdatedAt = DateTime.Now;
                        if (userId != 0) product.ResponsibleUserId = userId;
                    }

                    // Registrar Historial
                    var history = new InventoryHistory
                    {
                        ProductId = detail.ProductId,
                        MovementType = "Entrada",
                        Quantity = detail.Quantity,
                        UnitCost = detail.UnitCost,
                        SalePrice = detail.SalePrice,
                        ReferenceId = 0, // Se actualizará después o se maneja por WO
                        SupplierId = reception.SupplierId,
                        Observations = $"Recepción Masiva. {reception.Observations}",
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        ResponsibleUserId = userId != 0 ? userId : null
                    };
                    await _context.InventoryHistory.AddAsync(history, cancellation);
                }

                var result = await _context.SaveChangesAsync(cancellation) > 0;
                
                // Actualizar ReferenceId en historial con el ID de la recepción generada
                // Esto es opcional dependiendo de si necesitas la relación exacta en el historial
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la recepción de inventario");
                return false;
            }
        }
    }
}
