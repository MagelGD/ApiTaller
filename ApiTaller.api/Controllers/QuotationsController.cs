using ApiTaller.Domain.Dtos.Quotations;
using ApiTaller.Domain.Interfaces.Services.Quotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ApiTaller.Domain.Constants;
using ApiTaller.api.Filters;
using ApiTaller.api.Helpers;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequireTenantModule(ModuleConstants.Quotations)]
    public class QuotationsController : ControllerBase
    {
        private readonly IQuotationService _quotationService;
        private readonly ILogger<QuotationsController> _logger;

        public QuotationsController(IQuotationService quotationService, ILogger<QuotationsController> logger)
        {
            _quotationService = quotationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status, 
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate, 
            CancellationToken cancellation)
        {
            try
            {
                var result = await _quotationService.GetAllAsync(status, startDate, endDate, cancellation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al obtener listado de cotizaciones: {Error}", detailedError);
                return StatusCode(500, new { message = "Error al obtener cotizaciones", error = detailedError });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellation)
        {
            try
            {
                var quote = await _quotationService.GetByIdAsync(id, cancellation);
                if (quote == null) return NotFound(new { message = "Cotización no encontrada" });
                return Ok(quote);
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al obtener cotización {Id}: {Error}", id, detailedError);
                return StatusCode(500, new { message = "Error interno al consultar la cotización", error = detailedError });
            }
        }

        [HttpGet("public/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByPublicToken(string token, CancellationToken cancellation)
        {
            try
            {
                var quote = await _quotationService.GetByTokenAsync(token, cancellation);
                if (quote == null) return NotFound(new { message = "Cotización no encontrada o enlace inválido." });
                return Ok(quote);
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al obtener cotización pública con token {Token}: {Error}", token, detailedError);
                return StatusCode(500, new { message = "Error interno al cargar la cotización pública.", error = detailedError });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuotationCreateDto dto, CancellationToken cancellation)
        {
            try
            {
                var created = await _quotationService.CreateAsync(dto, cancellation);
                return Ok(created);
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al crear cotización: {Error}", detailedError);
                return StatusCode(500, new { message = "Error al crear la cotización", error = detailedError });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] QuotationCreateDto dto, CancellationToken cancellation)
        {
            try
            {
                bool updated = await _quotationService.UpdateAsync(id, dto, cancellation);
                if (!updated) return NotFound(new { message = "Cotización no encontrada" });
                return Ok(new { success = true, message = "Cotización actualizada correctamente" });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al actualizar cotización {Id}: {Error}", id, detailedError);
                return StatusCode(500, new { message = "Error al actualizar la cotización", error = detailedError });
            }
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendQuotationEmailDto dto, CancellationToken cancellation)
        {
            try
            {
                bool sent = await _quotationService.SendEmailAsync(dto, cancellation);
                return Ok(new { success = sent, message = "Cotización enviada exitosamente por correo" });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al enviar correo de cotización {QuotationId}: {Error}", dto.QuotationId, detailedError);
                return StatusCode(500, new { message = "Error al enviar el correo", error = detailedError });
            }
        }

        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id, [FromBody] QuotationApprovalRequestDto dto, CancellationToken cancellation)
        {
            try
            {
                bool approved = await _quotationService.ProcessApprovalAsync(id, dto, cancellation);
                return Ok(new { success = approved, message = "Cotización procesada exitosamente" });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al procesar aprobación para cotización {Id}: {Error}", id, detailedError);
                return StatusCode(500, new { message = "Error al procesar la aprobación", error = detailedError });
            }
        }

        [HttpPost("public/{token}/approve")]
        [AllowAnonymous]
        public async Task<IActionResult> PublicApprove(string token, [FromBody] QuotationApprovalRequestDto dto, CancellationToken cancellation)
        {
            try
            {
                bool approved = await _quotationService.ProcessPublicApprovalAsync(token, dto, cancellation);
                if (!approved) return NotFound(new { message = "Cotización no encontrada o expirada" });
                return Ok(new { success = true, message = "¡Gracias! Tu cotización ha sido aprobada con éxito. Nos pondremos en contacto contigo." });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al aprobar cotización pública con token {Token}: {Error}", token, detailedError);
                return StatusCode(500, new { message = "Error al procesar la aprobación pública", error = detailedError });
            }
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] RejectRequest request, CancellationToken cancellation)
        {
            try
            {
                bool rejected = await _quotationService.RejectQuotationAsync(id, request?.Reason, cancellation);
                return Ok(new { success = rejected, message = "Cotización rechazada" });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al rechazar cotización {Id}: {Error}", id, detailedError);
                return StatusCode(500, new { message = "Error al rechazar la cotización", error = detailedError });
            }
        }

        [HttpPost("convert-to-order")]
        public async Task<IActionResult> ConvertToOrder([FromBody] QuotationConvertToOrderDto dto, CancellationToken cancellation)
        {
            try
            {
                int orderId = await _quotationService.ConvertToWorkOrderAsync(dto, cancellation);
                return Ok(new { success = true, workOrderId = orderId, message = $"Cotización convertida en Orden de Trabajo #{orderId}" });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al convertir cotización a orden de trabajo: {Error}", detailedError);
                return StatusCode(500, new { message = "Error al convertir a orden de trabajo", error = detailedError });
            }
        }

        [HttpPost("convert-to-sale")]
        public async Task<IActionResult> ConvertToSale([FromBody] QuotationConvertToSaleDto request, CancellationToken cancellation)
        {
            try
            {
                int saleId = await _quotationService.ConvertToDirectSaleDtoAsync(request, cancellation);
                return Ok(new { success = true, saleId = saleId, message = $"Cotización convertida en Venta Directa #{saleId}" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al convertir cotización a venta directa: {Error}", detailedError);
                return StatusCode(500, new { message = "Error al convertir a venta directa", error = detailedError });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellation)
        {
            try
            {
                bool deleted = await _quotationService.DeleteAsync(id, cancellation);
                return Ok(new { success = deleted });
            }
            catch (Exception ex)
            {
                string detailedError = ExceptionHelper.GetDetailedMessage(ex);
                _logger.LogError(ex, "Error al eliminar cotización {Id}: {Error}", id, detailedError);
                return StatusCode(500, new { message = "Error al eliminar cotización", error = detailedError });
            }
        }
    }

    public class RejectRequest
    {
        public string? Reason { get; set; }
    }

    public class ConvertToSaleRequest
    {
        public int QuotationId { get; set; }
        public int PaymentMethodId { get; set; }
        public string? ReferenceCode { get; set; }
    }
}
