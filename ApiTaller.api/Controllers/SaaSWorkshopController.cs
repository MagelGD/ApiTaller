using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ApiTaller.Infrastructure.Data;

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

        public SaaSWorkshopController(IWorkshopOnboardingService onboardingService, IWorkshopService workshopService, DataContext context)
        {
            _onboardingService = onboardingService;
            _workshopService = workshopService;
            _context = context;
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
                object list = workshops.Select(w => new { id = w.Id, name = w.Name, type = w.WorkshopType });
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching workshops", error = ex.Message });
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
