using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace ApiTaller.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Lo ideal es proteger por rol: [Authorize(Roles = "SuperAdmin")]
    public class SaaSWorkshopController : ControllerBase
    {
        private readonly IWorkshopOnboardingService _onboardingService;
        private readonly IWorkshopService _workshopService;

        public SaaSWorkshopController(IWorkshopOnboardingService onboardingService, IWorkshopService workshopService)
        {
            _onboardingService = onboardingService;
            _workshopService = workshopService;
        }

        [HttpPost("onboarding")]
        public async Task<IActionResult> OnboardWorkshop([FromBody] WorkshopOnboardingRequestDto request)
        {
            if (request == null)
                return BadRequest("Invalid request.");

            try
            {
                var newWorkshopId = await _onboardingService.OnboardWorkshopAsync(request);
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
                var workshops = await _workshopService.GetAllAsync();
                var list = workshops.Select(w => new { id = w.Id, name = w.Name, type = w.WorkshopType });
                return Ok(list);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching workshops", error = ex.Message });
            }
        }
    }
}
