using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using ApiTaller.Domain.Constants;
using ApiTaller.api.Filters;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequireTenantModule(ModuleConstants.Inventory)]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _service;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener inventario completo");
            }
            return BadRequest();
        }

        [HttpGet("GetByProduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetByProductIdAsync(productId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener inventario por producto");
            }
            return BadRequest();
        }

        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.AddStockAsync(movement, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al añadir stock");
            }
            return BadRequest();
        }

        [HttpPost("RemoveStock")]
        public async Task<IActionResult> RemoveStock(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.RemoveStockAsync(movement, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al retirar stock");
            }
            return BadRequest();
        }

        [HttpPost("AdjustStock")]
        public async Task<IActionResult> AdjustStock(InventoryHistoryDto movement, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.AdjustStockAsync(movement, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ajustar stock");
            }
            return BadRequest();
        }

        [HttpGet("GetHistory/{productId}")]
        public async Task<IActionResult> GetHistory(int productId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetHistoryAsync(productId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historial de inventario");
            }
            return BadRequest();
        }
    }
}
