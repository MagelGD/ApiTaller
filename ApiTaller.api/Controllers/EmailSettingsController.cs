using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Dtos.WorkshopConfig;
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
    public class EmailSettingsController : ControllerBase
    {
        private readonly ILogger<EmailSettingsController> _logger;
        private readonly IEmailSettingsService _emailSettingsService;

        public EmailSettingsController(ILogger<EmailSettingsController> logger, IEmailSettingsService emailSettingsService)
        {
            _logger = logger;
            _emailSettingsService = emailSettingsService;
        }

        [HttpGet("GetEmailSettings")]
        public async Task<IActionResult> GetEmailSettings(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _emailSettingsService.GetSettingsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración de correo");
            }
            return BadRequest();
        }

        [HttpPost("SaveEmailSettings")]
        public async Task<IActionResult> SaveEmailSettings([FromBody] EmailSettingsDto dto, CancellationToken cancellation)
        {
            try
            {
                var result = await _emailSettingsService.SaveSettingsAsync(dto, cancellation);
                if (result)
                {
                    return Ok(true);
                }
                return BadRequest("No se pudo guardar la configuración de correo. Verifique los datos e intente nuevamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración de correo");
            }
            return BadRequest("Error interno al intentar guardar la configuración de correo.");
        }

        [HttpPost("TestEmailConnection")]
        public async Task<IActionResult> TestEmailConnection([FromBody] EmailSettingsDto dto, CancellationToken cancellation)
        {
            try
            {
                var result = await _emailSettingsService.TestConnectionAsync(dto, cancellation);
                if (result)
                {
                    return Ok(true);
                }
                return BadRequest("La prueba de conexión SMTP falló. Verifique los datos del servidor e intente de nuevo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la prueba de conexión SMTP");
            }
            return BadRequest("Error al intentar realizar la prueba de conexión SMTP.");
        }
    }
}
