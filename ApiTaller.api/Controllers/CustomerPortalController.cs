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
    [Authorize] // Todos los endpoints requieren JWT válido
    [Authorize]
    public class CustomerPortalController : ControllerBase
    {
        private readonly ICustomerPortalService _service;
        private readonly ILogger<CustomerPortalController> _logger;

        public CustomerPortalController(ICustomerPortalService service, ILogger<CustomerPortalController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Retorna los vehículos del cliente autenticado (filtrado por JWT).
        /// </summary>
        [HttpGet("MyVehicles")]
        public async Task<IActionResult> GetMyVehicles(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetMyVehiclesAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos del portal del cliente");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retorna las órdenes de un vehículo del cliente autenticado.
        /// Si el vehículo no pertenece al cliente, retorna lista vacía (no 403).
        /// </summary>
        [HttpGet("MyOrders/{vehicleId}")]
        public async Task<IActionResult> GetMyOrders(int vehicleId, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.GetMyOrdersByVehicleAsync(vehicleId, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener órdenes del vehículo {vehicleId} en el portal");
                return BadRequest();
            }
        }

        /// <summary>
        /// Retorna el detalle completo de una orden.
        /// Si la orden no pertenece al cliente, retorna 404 (sin confirmar existencia).
        /// </summary>
        [HttpGet("MyOrder/{orderId}")]
        public async Task<IActionResult> GetMyOrder(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _service.GetMyOrderDetailAsync(orderId, cancellationToken);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el detalle de la orden {orderId} en el portal");
                return BadRequest();
            }
        }

        /// <summary>
        /// Aprueba o rechaza un ítem de cotización (Part o Service).
        /// Verifica la cadena completa de pertenencia antes de modificar.
        /// </summary>
        [HttpPost("ApproveItem")]
        public async Task<IActionResult> ApproveItem(CustomerPortalApprovalDto dto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _service.ApproveItemAsync(dto, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar/rechazar ítem en el portal del cliente");
                return BadRequest();
            }
        }
    }
}
