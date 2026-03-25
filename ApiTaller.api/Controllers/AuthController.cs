using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Pots(Auth auth, CancellationToken cancellationToken)
        {
            return Ok(await _authService.Login(auth, cancellationToken) );
        }

        [Authorize]
        [HttpGet("Prueba")]
        public async Task<IActionResult> GetPrueba()
        {
            return Ok(new {message = "Prueba Exitosa"});
        }
    }
}
