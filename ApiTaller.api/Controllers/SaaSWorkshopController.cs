using ApiTaller.api.Hubs;
using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using ApiTaller.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaller.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaaSWorkshopController : ControllerBase
    {
        private readonly IWorkshopOnboardingService _onboardingService;
        private readonly IWorkshopService _workshopService;
        private readonly DataContext _context;
        private readonly IHubContext<PermissionsHub> _hubContext;

        public SaaSWorkshopController(
            IWorkshopOnboardingService onboardingService,
            IWorkshopService workshopService,
            DataContext context,
            IHubContext<PermissionsHub> hubContext)
        {
            _onboardingService = onboardingService;
            _workshopService = workshopService;
            _context = context;
            _hubContext = hubContext;
        }

        [HttpPost("onboarding")]
        public async Task<IActionResult> OnboardWorkshop([FromBody] WorkshopOnboardingRequestDto request)
        {
            if (request == null)
                return BadRequest("Invalid request.");

            try
            {
                int newWorkshopId = await _onboardingService.OnboardWorkshopAsync(request);
                return Ok(new { message = "Workshop created successfully", workshopId = newWorkshopId });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the workshop", error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetWorkshopsList()
        {
            try
            {
                IEnumerable<WorkshopDto> workshops = await _workshopService.GetAllAsync();
                object list = workshops.Select(w => new { id = w.Id, name = w.Name, type = w.WorkshopType, isActive = w.IsActive, plan = w.Plan });
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching workshops", error = ex.Message });
            }
        }

        [HttpGet("available-modules")]
        public async Task<IActionResult> GetAvailableModules()
        {
            try
            {
                var modules = await _workshopService.GetAvailableModulesAsync();
                return Ok(modules);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener el catálogo de módulos disponibles.", error = ex.Message });
            }
        }

        [HttpGet("{workshopId}/modules")]
        public async Task<IActionResult> GetWorkshopModules(int workshopId)
        {
            try
            {
                var modules = await _workshopService.GetWorkshopModulesAsync(workshopId);
                return Ok(modules);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener los módulos del taller {workshopId}.", error = ex.Message });
            }
        }

        [HttpPut("{workshopId}/modules")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateWorkshopModules(int workshopId, [FromBody] UpdateWorkshopModulesDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid payload.");

            try
            {
                bool success = await _workshopService.UpdateWorkshopModulesAsync(workshopId, dto);
                if (!success) return NotFound(new { message = $"No se encontró el taller con ID {workshopId}." });

                // Notificar a todos los clientes por SignalR
                await _hubContext.Clients.All.SendAsync("WorkshopModulesChanged", workshopId);

                var workshopRoles = await _context.UserRole
                    .IgnoreQueryFilters()
                    .Where(r => r.WorkshopId == workshopId)
                    .Select(r => r.Id)
                    .ToListAsync();

                foreach (var roleId in workshopRoles)
                {
                    await _hubContext.Clients.All.SendAsync("PermissionsChanged", roleId);
                }

                return Ok(new { message = "Módulos del taller actualizados exitosamente." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = $"Error al actualizar módulos del taller {workshopId}.", error = ex.Message });
            }
        }

        [HttpGet("metrics")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetGlobalMetrics()
        {
            try
            {
                int totalWorkshops = await _context.Workshop.CountAsync();
                int totalUsers = await _context.User.CountAsync();
                int activeWorkshops = await _context.Workshop.Where(w => w.IsActive).CountAsync();
                
                return Ok(new {
                    TotalWorkshops = totalWorkshops,
                    ActiveWorkshops = activeWorkshops,
                    TotalUsers = totalUsers,
                    SystemHealth = "Optimo"
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching metrics", error = ex.Message });
            }
        }
    }
}
