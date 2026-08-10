using ApiTaller.Domain.Dtos.Dashboard;
using ApiTaller.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("AdminStats")]
        [ProducesResponseType(typeof(AdminDashboardStatsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminStats(CancellationToken ct)
        {
            try
            {
                AdminDashboardStatsDto stats = await _dashboardService.GetAdminStatsAsync(ct);
                return Ok(stats);
            }
            catch (OperationCanceledException)
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error loading dashboard stats", error = ex.Message });
            }
        }
    }
}
