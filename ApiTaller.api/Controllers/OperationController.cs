using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Interfaces.Services.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationController : ControllerBase
    {
        private readonly ILogger<OperationController> _logger;
        private readonly IOperationService _operationService;

        public OperationController(ILogger<OperationController> logger, IOperationService operationService)
        {
            _logger = logger;
            _operationService = operationService;
        }

        [HttpGet("GetOperations")]
        public async Task<IActionResult> GetOperations(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _operationService.GetOperations(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las operaciones");
            }
            return BadRequest();
        }

        [HttpGet("GetOperation/{id}")]
        public async Task<IActionResult> GetOperationById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _operationService.GetOperationsById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la operación por id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditOperation")]
        public async Task<IActionResult> SaveOrEditOperation(GetOperationDto getOperation, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _operationService.SaveOrEditOperation(getOperation, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar la operación");
            }
            return BadRequest();
        }
    }
}
