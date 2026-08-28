using ApiTaller.api.Filters;
using ApiTaller.Domain.Constants;
using ApiTaller.Domain.Dtos.Credits;
using ApiTaller.Domain.Interfaces.Services.Credits;
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
    [RequireTenantModule(ModuleConstants.Credits)]
    public class CreditsController : ControllerBase
    {
        private readonly ICreditService _service;
        private readonly ILogger<CreditsController> _logger;

        public CreditsController(ICreditService service, ILogger<CreditsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("customers-summary")]
        public async Task<IActionResult> GetCustomersWithCredit(CancellationToken cancellation)
        {
            try
            {
                var result = await _service.GetCustomersWithCreditAsync(cancellation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar cartera de clientes con crédito");
                return StatusCode(500, new { message = "Error al consultar la cartera de créditos." });
            }
        }

        [HttpGet("customer/{customerId}/statement")]
        public async Task<IActionResult> GetCustomerStatement(int customerId, CancellationToken cancellation)
        {
            try
            {
                var result = await _service.GetCustomerStatementAsync(customerId, cancellation);
                if (result == null)
                {
                    return NotFound(new { message = $"No se encontró el cliente con ID {customerId}." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al consultar extracto del cliente {customerId}");
                return StatusCode(500, new { message = "Error al consultar el extracto de cuenta." });
            }
        }

        [HttpPost("payment")]
        public async Task<IActionResult> RegisterPayment([FromBody] RegisterCreditPaymentDto dto, CancellationToken cancellation)
        {
            try
            {
                var success = await _service.RegisterPaymentAsync(dto, cancellation);
                if (success)
                {
                    return Ok(new { success = true, message = "Abono registrado exitosamente y saldo actualizado." });
                }
                return BadRequest(new { success = false, message = "No fue posible registrar el abono." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar abono a crédito");
                return StatusCode(500, new { success = false, message = "Error interno al procesar el abono." });
            }
        }
    }
}
