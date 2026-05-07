using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersController : ControllerBase
    {
        private readonly ILogger<WorkOrdersController> _logger;
        private readonly IWorkOrderService _workOrderService;

        public WorkOrdersController(IWorkOrderService workOrderService, ILogger<WorkOrdersController> logger)
        {
            _workOrderService = workOrderService;
            _logger = logger;
        }

        [HttpGet("GetWorkOrders")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workOrderService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las órdenes de trabajo");
            }
            return BadRequest();
        }

        [HttpGet("GetWorkOrder/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workOrderService.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la orden de trabajo con id {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditWorkOrder")]
        public async Task<IActionResult> Post(WorkOrderDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workOrderService.SaveAsync(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la orden de trabajo");
            }
            return BadRequest();
        }

        [HttpPost("ChangeStatus/{id}/{status}")]
        public async Task<IActionResult> ChangeStatus(int id, string status, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workOrderService.ChangeStatusAsync(id, status, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cambiar el estado de la orden {id}");
            }
            return BadRequest();
        }

        [HttpGet("GetHistory/{workOrderId}")]
        public async Task<IActionResult> GetHistory(int workOrderId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workOrderService.GetHistoryAsync(workOrderId, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el historial de la orden {workOrderId}");
            }
            return BadRequest();
        }
    }
}
