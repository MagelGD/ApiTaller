using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopService _workshopService;

        public WorkshopController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }

        /// <summary>
        /// Endpoint público para registro de un nuevo taller (onboarding SaaS).
        /// Crea el tenant, el administrador, y la configuración base.
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterWorkshopDto dto, CancellationToken ct)
        {
            bool result = await _workshopService.RegisterWorkshopAsync(dto, ct);
            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize] // PlatformAdmin o el propio Admin del Taller (validación en un atributo o policy no incluida para simplificar)
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            WorkshopDto? workshop = await _workshopService.GetByIdAsync(id, ct);
            if (workshop == null) return NotFound(new { Message = "Taller no encontrado." });
            return Ok(workshop);
        }

        [HttpGet]
        [Authorize] // Asume validación de PlatformAdmin
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            IEnumerable<WorkshopDto> workshops = await _workshopService.GetAllAsync(ct);
            return Ok(workshops);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkshopDto dto, CancellationToken ct)
        {
            try
            {
                bool success = await _workshopService.UpdateWorkshopAsync(id, dto, ct);
                if (!success) return NotFound(new { Message = "Taller no encontrado o no se pudo actualizar." });
                return Ok(new { Message = "Taller actualizado exitosamente." });
            }
            catch (InvalidOperationException ex)
            {
                // Maneja la validación de cambio de tipo de negocio fallida
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/validate-type-change")]
        [Authorize]
        public async Task<IActionResult> ValidateTypeChange(int id, [FromQuery] string newType, CancellationToken ct)
        {
            TypeChangeValidationDto validation = await _workshopService.ValidateTypeChangeAsync(id, newType, ct);
            return Ok(validation);
        }

        [HttpPatch("{id}/toggle-status")]
        [Authorize] // PlatformAdmin
        public async Task<IActionResult> ToggleStatus(int id, [FromBody] ToggleWorkshopStatusDto dto, CancellationToken ct)
        {
            bool success = await _workshopService.ToggleStatusAsync(id, dto.IsActive, ct);
            if (!success) return NotFound(new { Message = "Taller no encontrado." });
            return Ok(new { Message = "Estado del taller actualizado exitosamente." });
        }
    }
}
