using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Interfaces.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Auth;

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
        [EnableRateLimiting("LoginPolicy")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthDto auth, CancellationToken cancellation)
        {
            try
            {
                IncomeDto result = await _authService.Login(auth, cancellation);
                if (result == null)
                {
                    return Unauthorized(new { message = "Credenciales inválidas. Por favor, inténtalo de nuevo." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de Login");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [EnableRateLimiting("LoginPolicy")]
        [HttpPost("login-mobile")]
        public async Task<IActionResult> LoginMobile([FromBody] ApiTaller.Domain.Dtos.Auth.LoginMobileDto credentials, CancellationToken cancellation)
        {
            try
            {
                LoginResponseDto result = await _authService.LoginAsync(credentials, cancellation);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el login móvil");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ApiTaller.Domain.Dtos.Auth.ForgotPasswordDto dto, CancellationToken cancellation)
        {
            try
            {
                bool success = await _authService.GeneratePasswordResetTokenAsync(dto.Email, cancellation);
                if (success)
                {
                    return Ok(new { message = "Se ha enviado un correo para restablecer la contraseña si el correo está registrado." });
                }
                return BadRequest(new { message = "Error al procesar la solicitud." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en forgot-password");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ApiTaller.Domain.Dtos.Auth.ResetPasswordDto dto, CancellationToken cancellation)
        {
            try
            {
                bool success = await _authService.ResetPasswordAsync(dto, cancellation);
                if (success)
                {
                    return Ok(new { message = "Contraseña restablecida exitosamente." });
                }
                return BadRequest(new { message = "El token es inválido o ha expirado." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en reset-password");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ApiTaller.Domain.Dtos.Auth.ChangePasswordDto dto, CancellationToken cancellation)
        {
            try
            {
                string? sidClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Sid)?.Value;
                if (string.IsNullOrEmpty(sidClaim) || !int.TryParse(sidClaim, out int userId))
                {
                    return Unauthorized(new { message = "Usuario no identificado." });
                }

                bool success = await _authService.ChangePasswordAsync(userId, dto, cancellation);
                if (success)
                {
                    return Ok(new { message = "Contraseña cambiada exitosamente." });
                }
                return BadRequest(new { message = "No se pudo cambiar la contraseña." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en change-password");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

      
        [HttpGet("Pruebo")]
        public async Task<IActionResult> GetPrueba(CancellationToken cancellation)
        {
            try
            {
                return Ok(new { Message = "Prueba Exitosaaaaa" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en prueba de autorización");
            }
            return BadRequest();
        }
    }
}
