using ApiTaller.Domain.Dtos.Portal;
using ApiTaller.Domain.Dtos.Quotations;
using ApiTaller.Domain.Interfaces.Services.Portal;
using ApiTaller.Domain.Interfaces.Services.Quotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PortalController : ControllerBase
    {
        private readonly ILogger<PortalController> _logger;
        private readonly IPortalService _portalService;
        private readonly IQuotationService _quotationService;

        public PortalController(
            ILogger<PortalController> logger, 
            IPortalService portalService,
            IQuotationService quotationService)
        {
            _logger = logger;
            _portalService = portalService;
            _quotationService = quotationService;
        }

        private int GetCustomerIdFromUser()
        {
            string? customerIdClaim = User.FindFirst("customerId")?.Value;
            if (string.IsNullOrEmpty(customerIdClaim) || !int.TryParse(customerIdClaim, out int customerId))
            {
                throw new UnauthorizedAccessException("El usuario no tiene un rol de cliente válido asociado en su sesión.");
            }
            return customerId;
        }

        [HttpGet("mis-ordenes")]
        public async Task<IActionResult> GetMyOrders(CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                IEnumerable<PortalOrderListDto> orders = await _portalService.GetMyOrdersAsync(customerId, cancellation);
                return Ok(orders);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las órdenes desde el portal");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("mis-vehiculos")]
        public async Task<IActionResult> GetMyVehicles(CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                IEnumerable<PortalVehicleDto> vehicles = await _portalService.GetMyVehiclesAsync(customerId, cancellation);
                return Ok(vehicles);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos desde el portal");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("orden/{id}")]
        public async Task<IActionResult> GetOrderDetail(int id, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                PortalOrderDetailDto? order = await _portalService.GetOrderDetailAsync(id, customerId, cancellation);
                if (order == null)
                {
                    // Regla clave: retornar 403 Forbidden si el customer_id no coincide con el dueño de la orden
                    return Forbid("No tienes permisos para acceder a esta orden.");
                }
                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el detalle de la orden {OrderId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost("orden/{id}/aprobar")]
        public async Task<IActionResult> ApproveOrderItems(int id, [FromBody] PortalApproveItemsDto dto, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                bool success = await _portalService.ApproveOrderItemsAsync(id, customerId, dto, cancellation);
                if (!success)
                {
                    return Forbid("No tienes permisos para aprobar esta orden o la orden no se encuentra disponible.");
                }
                return Ok(new { message = "Ítems procesados y presupuesto actualizado correctamente." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar ítems para la orden {OrderId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("orden/{id}/factura")]
        public async Task<IActionResult> GetInvoicePdf(int id, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                PortalOrderDetailDto? order = await _portalService.GetOrderDetailAsync(id, customerId, cancellation);
                if (order == null)
                {
                    return Forbid("No tienes permisos para acceder a la factura de esta orden.");
                }

                // Generar un mock de PDF con cabecera válida de PDF
                byte[] pdfBytes = new byte[] { 
                    0x25, 0x50, 0x44, 0x46, 0x2d, 0x31, 0x2e, 0x34, // %PDF-1.4
                    0x0a, 0x25, 0xe2, 0xe3, 0xcf, 0xd3, 0x0a, 0x0a  // bin markers
                };
                
                return File(pdfBytes, "application/pdf", $"Factura_Orden_{id}.pdf");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al descargar factura para la orden {OrderId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("orden/{id}/fotos")]
        public async Task<IActionResult> GetOrderPhotos(int id, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                PortalOrderDetailDto? order = await _portalService.GetOrderDetailAsync(id, customerId, cancellation);
                if (order == null)
                {
                    return Forbid("No tienes permisos para acceder a las fotos de esta orden.");
                }
                return Ok(order.Evidences);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las fotos de la orden {OrderId}", id);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        // ==================== SECCIÓN DE COTIZACIONES DEL PORTAL ====================

        [HttpGet("mis-cotizaciones")]
        public async Task<IActionResult> GetMyQuotations(CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                var quotations = await _quotationService.GetMyQuotationsAsync(customerId, cancellation);
                return Ok(quotations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cotizaciones del cliente");
                return StatusCode(500, new { message = "Error al obtener cotizaciones" });
            }
        }

        [HttpGet("cotizacion/{id}")]
        public async Task<IActionResult> GetQuotationDetail(int id, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                var quote = await _quotationService.GetByIdAsync(id, cancellation);
                if (quote == null || quote.CustomerId != customerId)
                {
                    return Forbid("No tienes permisos para acceder a esta cotización.");
                }
                return Ok(quote);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener detalle de la cotización {Id}", id);
                return StatusCode(500, new { message = "Error al obtener cotización" });
            }
        }

        [HttpPost("cotizacion/{id}/aprobar")]
        public async Task<IActionResult> ApproveQuotation(int id, [FromBody] QuotationApprovalRequestDto dto, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                var quote = await _quotationService.GetByIdAsync(id, cancellation);
                if (quote == null || quote.CustomerId != customerId)
                {
                    return Forbid("No tienes permisos para aprobar esta cotización.");
                }

                bool approved = await _quotationService.ProcessApprovalAsync(id, dto, cancellation);
                return Ok(new { success = approved, message = "Cotización aprobada correctamente" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aprobar cotización {Id}", id);
                return StatusCode(500, new { message = "Error al aprobar la cotización" });
            }
        }

        [HttpPost("cotizacion/{id}/rechazar")]
        public async Task<IActionResult> RejectQuotation(int id, [FromBody] RejectRequest request, CancellationToken cancellation)
        {
            try
            {
                int customerId = GetCustomerIdFromUser();
                var quote = await _quotationService.GetByIdAsync(id, cancellation);
                if (quote == null || quote.CustomerId != customerId)
                {
                    return Forbid("No tienes permisos para rechazar esta cotización.");
                }

                bool rejected = await _quotationService.RejectQuotationAsync(id, request?.Reason, cancellation);
                return Ok(new { success = rejected, message = "Cotización rechazada" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al rechazar cotización {Id}", id);
                return StatusCode(500, new { message = "Error al rechazar cotización" });
            }
        }
    }
}
