using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
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
        public async Task<IActionResult> SaveEmailSettings([FromBody] EmailSettings settings, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _emailSettingsService.SaveSettingsAsync(settings, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración de correo");
            }
            return BadRequest();
        }

        [HttpPost("TestEmailConnection")]
        public async Task<IActionResult> TestEmailConnection([FromBody] EmailSettings settings, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _emailSettingsService.TestConnectionAsync(settings, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en la prueba de conexión SMTP");
            }
            return BadRequest();
        }
    }
}
