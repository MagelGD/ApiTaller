using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkOrdersController : ControllerBase
    {
        private readonly ILogger<WorkOrdersController> _logger;
        private readonly IWorkOrderService _workOrderService;

        public WorkOrdersController(ILogger<WorkOrdersController> logger, IWorkOrderService workOrderService)
        {
            _logger = logger;
            _workOrderService = workOrderService;
        }

        [HttpGet("GetWorkOrders")]
        public async Task<IActionResult> GetWorkOrders(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _workOrderService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ordenes de trabajo");
            }
            return BadRequest();
        }

        [HttpGet("GetWorkOrderById/{id}")]
        [HttpGet("GetWorkOrder/{id}")]
        public async Task<IActionResult> GetWorkOrderById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _workOrderService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la orden de trabajo con id {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditWorkOrder")]
        public async Task<IActionResult> CreateOrEditWorkOrder(WorkOrderDto value, CancellationToken cancellation)
        {
            try
            {
                var success = await _workOrderService.SaveAsync(value, cancellation);
                if (!success)
                {
                    return BadRequest("No se pudo guardar la orden de trabajo (verifique que los datos sean correctos).");
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la orden de trabajo");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ChangeStatus/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int id, string status, CancellationToken cancellation)
        {
            try
            {
                var success = await _workOrderService.ChangeStatusAsync(id, status, cancellation);
                if (!success)
                {
                    return BadRequest("No se pudo cambiar el estado de la orden (verifique que tenga repuestos o servicios).");
                }
                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cambiar el estado de la orden {id}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetHistory/{workOrderId}")]
        public async Task<IActionResult> GetHistory(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _workOrderService.GetHistoryAsync(workOrderId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el historial de la orden {workOrderId}");
            }
            return BadRequest();
        }

        [HttpPost("AddEvidence")]
        public async Task<IActionResult> AddEvidence(WorkOrderEvidenceDto value, CancellationToken cancellation)
        {
            try
            {
                var result = await _workOrderService.AddEvidenceAsync(value, cancellation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar evidencia individual");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteEvidence/{id}")]
        public async Task<IActionResult> DeleteEvidence(int id, CancellationToken cancellation)
        {
            try
            {
                var success = await _workOrderService.DeleteEvidenceAsync(id, cancellation);
                if (!success)
                {
                    return BadRequest("No se pudo eliminar la evidencia individual.");
                }
                return Ok(success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la evidencia individual {id}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
