using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Interfaces.Services.Operations;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ILogger<OperationController> _logger;
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService, ILogger<OperationController> logger)
        {
            _operationService = operationService;
            _logger = logger;
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

        [HttpGet("GetOperartions/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
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
        public async Task<IActionResult> SaveOrEdit(GetOperation getOperation, CancellationToken cancellation)
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
