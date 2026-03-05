using ApiTaller.Domain.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAuthService _authService;

        public WeatherForecastController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Gets(CancellationToken cancellationToken)
        {
            await _authService.Login("MagelAdmin", "123456", cancellationToken);
            return Ok();
        }
    }
}
