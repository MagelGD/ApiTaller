using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthDto auth, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _authService.Login(auth, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de Login");
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("Prueba")]
        public async Task<IActionResult> GetPrueba(CancellationToken cancellation)
        {
            try
            {
                return Ok(new { Message = "Prueba Exitosa" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en prueba de autorización");
            }
            return BadRequest();
        }
    }
}
