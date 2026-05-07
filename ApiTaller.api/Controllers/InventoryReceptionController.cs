using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryReceptionController : ControllerBase
    {
        private readonly IInventoryReceptionService _receptionService;
        private readonly ILogger<InventoryReceptionController> _logger;

        public InventoryReceptionController(IInventoryReceptionService receptionService, ILogger<InventoryReceptionController> logger)
        {
            _receptionService = receptionService;
            _logger = logger;
        }

        [HttpGet("GetReceptions")]
        public async Task<IActionResult> GetReceptions(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _receptionService.GetReceptionsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las recepciones de inventario");
            }
            return BadRequest();
        }

        [HttpPost("SaveReception")]
        public async Task<IActionResult> SaveReception([FromBody] InventoryReceptionDto dto, CancellationToken cancellation)
        {
            try
            {
                if (dto == null) return BadRequest("Datos inválidos");
                var result = await _receptionService.SaveReceptionAsync(dto, cancellation);
                if (result) return Ok(new { message = "Recepción registrada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la recepción de inventario");
            }
            return BadRequest();
        }
    }
}
