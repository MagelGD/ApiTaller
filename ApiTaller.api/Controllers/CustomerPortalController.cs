using ApiTaller.Domain.Dtos.CustomerPortal;
using ApiTaller.Domain.Interfaces.Services.CustomerPortal;
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
    public class CustomerPortalController : ControllerBase
    {
        private readonly ILogger<CustomerPortalController> _logger;
        private readonly ICustomerPortalService _service;

        public CustomerPortalController(ILogger<CustomerPortalController> logger, ICustomerPortalService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("MyVehicles")]
        public async Task<IActionResult> GetMyVehicles(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetMyVehiclesAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vehiculos del portal del cliente");
            }
            return BadRequest();
        }

        [HttpGet("MyOrders/{vehicleId}")]
        public async Task<IActionResult> GetMyOrders(int vehicleId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetMyOrdersByVehicleAsync(vehicleId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ordenes del portal del cliente");
            }
            return BadRequest();
        }

        [HttpGet("MyOrder/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetMyOrderDetailAsync(orderId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalles de orden del portal del cliente");
            }
            return BadRequest();
        }

        [HttpPost("ApproveOrder/{orderId}")]
        public async Task<IActionResult> ApproveOrder(int orderId, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.ApproveFullOrderAsync(orderId, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar orden del portal del cliente");
            }
            return BadRequest();
        }

        [HttpPost("RegisterVehicle")]
        public async Task<IActionResult> RegisterVehicle([FromBody] CustomerPortalCreateVehicleDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.CreateMyVehicleAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar vehculo en el portal del cliente");
            }
            return BadRequest();
        }

        [HttpGet("MyAppointments")]
        public async Task<IActionResult> GetMyAppointments(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _service.GetMyAppointmentsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener citas del portal del cliente");
            }
            return BadRequest();
        }
    }
}
