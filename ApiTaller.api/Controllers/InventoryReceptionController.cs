using ApiTaller.Domain.Dtos.Inventory;
using ApiTaller.Domain.Interfaces.Services.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class InventoryReceptionController : ControllerBase
    {
        private readonly ILogger<InventoryReceptionController> _logger;
        private readonly IInventoryReceptionService _receptionService;

        public InventoryReceptionController(ILogger<InventoryReceptionController> logger, IInventoryReceptionService receptionService)
        {
            _logger = logger;
            _receptionService = receptionService;
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
                return Ok(await _receptionService.SaveReceptionAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la recepción de inventario");
            }
            return BadRequest();
        }
    }
}
