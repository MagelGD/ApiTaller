using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllAsync(cancellationToken));
        }

        [HttpGet("GetByProduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetByProductIdAsync(productId, cancellationToken));
        }

        [HttpPost("AddStock")]
        public async Task<IActionResult> AddStock(InventoryHistoryDto movement, CancellationToken cancellationToken)
        {
            return Ok(await _service.AddStockAsync(movement, cancellationToken));
        }

        [HttpPost("RemoveStock")]
        public async Task<IActionResult> RemoveStock(InventoryHistoryDto movement, CancellationToken cancellationToken)
        {
            return Ok(await _service.RemoveStockAsync(movement, cancellationToken));
        }

        [HttpPost("AdjustStock")]
        public async Task<IActionResult> AdjustStock(InventoryHistoryDto movement, CancellationToken cancellationToken)
        {
            return Ok(await _service.AdjustStockAsync(movement, cancellationToken));
        }

        [HttpGet("GetHistory/{productId}")]
        public async Task<IActionResult> GetHistory(int productId, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetHistoryAsync(productId, cancellationToken));
        }
    }
}
