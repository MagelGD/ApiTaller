using ApiTaller.Domain.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

      

        [HttpGet]
        public IActionResult Gets()
        {
            _authService.Login("MagelAdmin", "admin");
            return Ok();
        }
    }
}
