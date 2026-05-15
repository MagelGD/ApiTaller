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
        private readonly ILogger<WorkshopSettingsController> _logger;
        private readonly IWorkshopSettingsService _workshopSettingsService;

        public WorkshopSettingsController(ILogger<WorkshopSettingsController> logger, IWorkshopSettingsService workshopSettingsService)
        {
            _logger = logger;
            _workshopSettingsService = workshopSettingsService;
        }

        [HttpGet("GetWorkshopSetting")]
        public async Task<IActionResult> GetWorkshopSetting(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _workshopSettingsService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración del taller");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditWorkshopSetting")]
        public async Task<IActionResult> CreateOrEditWorkshopSetting(WorkshopSettingsDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _workshopSettingsService.UpsertAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración del taller");
            }
            return BadRequest();
        }
    }
}
