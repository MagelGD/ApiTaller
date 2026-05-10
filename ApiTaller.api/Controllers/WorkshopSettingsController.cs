using ApiTaller.Domain.Dtos.WorkshopConfig;
using ApiTaller.Domain.Interfaces.Services.WorkshopSettings;
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
    public class WorkshopSettingsController : ControllerBase
    {
        private readonly IWorkshopSettingsService _workshopSettingsService;
        private readonly ILogger<WorkshopSettingsController> _logger;

        public WorkshopSettingsController(IWorkshopSettingsService workshopSettingsService, ILogger<WorkshopSettingsController> logger)
        {
            _workshopSettingsService = workshopSettingsService;
            _logger = logger;
        }

        [HttpGet("GetWorkshopSettingByKey/{key}")]
        public async Task<IActionResult> GetWorkshopSettingByKey(string key, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _workshopSettingsService.GetByKeyAsync(key, cancellationToken);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la configuración con clave '{key}'");
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpPost("CreateOrEditWorkshopSetting")]
        public async Task<IActionResult> CreateOrEditWorkshopSetting(WorkshopSettingsDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _workshopSettingsService.UpsertAsync(dto, cancellationToken);
                if (!result)
                {
                    return BadRequest(new { message = "No se pudo guardar la configuración. Verifique los datos." });
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración del taller");
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }
    }
}
