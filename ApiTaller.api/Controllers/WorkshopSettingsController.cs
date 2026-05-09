using ApiTaller.Domain.Dtos.WorkshopConfig;
using ApiTaller.Domain.Interfaces.Services.WorkshopSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopSettingsController : ControllerBase
    {
        private readonly IWorkshopSettingsService _workshopSettingsService;
        private readonly ILogger<WorkshopSettingsController> _logger;

        public WorkshopSettingsController(IWorkshopSettingsService workshopSettingsService, ILogger<WorkshopSettingsController> logger)
        {
            _workshopSettingsService = workshopSettingsService;
            _logger = logger;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetByKey(string key, CancellationToken cancellationToken)
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
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(WorkshopSettingsDto dto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workshopSettingsService.UpsertAsync(dto, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración del taller");
                return BadRequest();
            }
        }
    }
}
