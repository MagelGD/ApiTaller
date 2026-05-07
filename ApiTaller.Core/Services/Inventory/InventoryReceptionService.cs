using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Repositories.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Inventory
{
    public class InventoryReceptionService : IInventoryReceptionService
    {
        private readonly IInventoryReceptionRepository _receptionRepository;
        private readonly ILogger<InventoryReceptionService> _logger;

        public InventoryReceptionService(IInventoryReceptionRepository receptionRepository, ILogger<InventoryReceptionService> logger)
        {
            _receptionRepository = receptionRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<InventoryReceptionDto>> GetReceptionsAsync(CancellationToken cancellation)
        {
            try
            {
                var result = await _receptionRepository.GetAllAsync(cancellation);
                return result.Select(r => new InventoryReceptionDto
                {
                    Id = r.Id,
                    SupplierId = r.SupplierId,
                    SupplierName = r.SupplierNavigation?.BusinessName ?? "Proveedor Externo",
                    ReceptionDate = r.ReceptionDate,
                    Observations = r.Observations,
                    InvoiceImageBase64 = r.InvoiceImageBase64,
                    TotalAmount = r.TotalAmount,
                    Details = r.Details.Select(d => new InventoryReceptionDetailDto
                    {
                        ProductId = d.ProductId,
                        ProductName = d.ProductNavigation?.ProductName ?? "Producto no encontrado",
                        Quantity = d.Quantity,
                        UnitCost = d.UnitCost,
                        SalePrice = d.SalePrice
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el historial de recepciones en el servicio");
                return new List<InventoryReceptionDto>();
            }
        }

        public async Task<bool> SaveReceptionAsync(InventoryReceptionDto value, CancellationToken cancellation)
        {
            try
            {
                var reception = new InventoryReception
                {
                    SupplierId = value.SupplierId,
                    Observations = value.Observations,
                    InvoiceImageBase64 = value.InvoiceImageBase64,
                    Details = value.Details.Select(d => new InventoryReceptionDetail
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitCost = d.UnitCost,
                        SalePrice = d.SalePrice
                    }).ToList()
                };

                return await _receptionRepository.CreateAsync(reception, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la recepción de inventario en el servicio");
                return false;
            }
        }
    }
}
