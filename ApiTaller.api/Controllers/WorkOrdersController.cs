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
                return Ok(await _workOrderService.SaveAsync(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la orden de trabajo");
            }
            return BadRequest();
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
            }
            return BadRequest();
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
    }
}
