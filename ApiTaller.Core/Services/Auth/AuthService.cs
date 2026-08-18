using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Dtos.Auth;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Login;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Interfaces.Services.Session;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Helpers.Jwt;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ApiTaller.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtOptions _options;
        private readonly ILoginService _loginService;
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        private readonly IMemoryCache _memoryCache;
        private readonly ISessionNotificationService _sessionNotificationService;

        public AuthService(
            IUserService userService, 
            ILogger<AuthService> logger, 
            IOptions<JwtOptions> options, 
            ILoginService loginService,
            DataContext context,
            IEmailService emailService,
            IMemoryCache memoryCache,
            ISessionNotificationService sessionNotificationService)
        {
            _userService = userService;
            _logger = logger;
            _options = options.Value;
            _loginService = loginService;
            _context = context;
            _emailService = emailService;
            _memoryCache = memoryCache;
            _sessionNotificationService = sessionNotificationService;
        }

        public async Task<IncomeDto> Login(AuthDto auth, CancellationToken cancellation = default)
        {
            try
            {
                LoginUserDto? user = await _userService.GetUser(auth.Username, cancellation);
                if (user is null || !BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
                    return default!;

                JwtResult jwtResult = user.CreateJwt(_options);
                if (string.IsNullOrEmpty(jwtResult.Token))
                    return default!;

                // 1. Notificar expulsión en tiempo real a sesiones previas
                await _sessionNotificationService.NotifyForceLogoutAsync(user.Id, "Se ha iniciado sesión desde otro dispositivo.");

                // 2. Guardar el JTI en User.Token y en IMemoryCache como sesión activa
                user.Token = jwtResult.Jti;
                user.ExpireToken = _options.AccessTokenMinutes;

                if (!await _userService.UpdateUserToken(user, cancellation))
                    return default!;

                _memoryCache.Set($"active_session_user_{user.Id}", jwtResult.Jti, TimeSpan.FromMinutes(_options.AccessTokenMinutes));

                if (!await _loginService.AddUserLogin(user, cancellation))
                    return default!;

                IncomeDto income = new()
                {
                    Fullname = user.Fullname,
                    Token = jwtResult.Token,
                    Success = true,
                    IdUser = user.Id,
                    IdRoleUser = user.IdUserRole ?? 0
                };
                return income;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en el proceso de Login");
                return default!;
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginMobileDto credentials, CancellationToken ct)
        {
            try
            {
                User? user = await _context.User
                    .Include(u => u.UserRoleIdNavigation)
                    .Include(u => u.WorkshopNavigation)
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == credentials.Email.ToLower() && u.IsActive, ct);

                if (user == null || !BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password))
                {
                    throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");
                }

                // Buscar el cliente asociado
                Domain.Models.Customer? customer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.UserId == user.Id && c.IsActive, ct);

                JwtResult jwtResult = user.CreateJwt(customer?.Id, _options);

                // 1. Notificar expulsión en tiempo real a sesiones previas
                await _sessionNotificationService.NotifyForceLogoutAsync(user.Id, "Se ha iniciado sesión desde otro dispositivo.");

                // 2. Guardar el JTI en BD y en IMemoryCache como sesión activa
                user.Token = jwtResult.Jti;
                user.ExpirationDate = DateTime.Now.AddMinutes(_options.AccessTokenMinutes);
                user.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync(ct);

                _memoryCache.Set($"active_session_user_{user.Id}", jwtResult.Jti, TimeSpan.FromMinutes(_options.AccessTokenMinutes));

                return new LoginResponseDto
                {
                    Token = jwtResult.Token,
                    Role = user.UserRoleIdNavigation?.Role?.ToLower() ?? "cliente",
                    MustChangePassword = user.MustChangePassword,
                    UserId = user.Id,
                    CustomerId = customer?.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el inicio de sesión móvil para {Email}", credentials.Email);
                throw;
            }
        }

        public async Task<bool> LogoutAsync(int userId, CancellationToken ct = default)
        {
            try
            {
                _memoryCache.Remove($"active_session_user_{userId}");
                await _context.User
                    .Where(u => u.Id == userId)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(p => p.Token, (string?)null)
                        .SetProperty(p => p.UpdatedAt, DateTime.Now), ct);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar logout para usuario {UserId}", userId);
                return false;
            }
        }

        public async Task<bool> GeneratePasswordResetTokenAsync(string email, CancellationToken ct)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException("El correo electrónico es requerido.");
                }

                string normalizedEmail = email.Trim().ToLower();

                User? user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == normalizedEmail && u.IsActive, ct);

                if (user == null)
                {
                    throw new KeyNotFoundException("El correo electrónico no se encuentra registrado en el sistema.");
                }

                // Inactivar tokens de recuperación activos anteriores
                List<PasswordResetToken> previousTokens = await _context.PasswordResetToken
                    .Where(t => t.UserId == user.Id && t.IsActive && !t.IsUsed)
                    .ToListAsync(ct);

                foreach (PasswordResetToken t in previousTokens)
                {
                    t.IsActive = false;
                    t.UpdatedAt = DateTime.Now;
                }

                string tokenString = Guid.NewGuid().ToString("N");
                PasswordResetToken resetToken = new PasswordResetToken
                {
                    UserId = user.Id,
                    Token = tokenString,
                    ExpirationDate = DateTime.Now.AddHours(1),
                    IsUsed = false,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = user.Id
                };

                await _context.PasswordResetToken.AddAsync(resetToken, ct);
                bool saved = await _context.SaveChangesAsync(ct) > 0;

                if (saved)
                {
                    // Enviar correo de recuperación con plantilla oficial GarageMotor
                    string resetUrl = $"https://garagemotor.co/reset-password?token={tokenString}";
                    EmailRequest emailRequest = new EmailRequest
                    {
                        To = user.Email,
                        Subject = "Recupera tu contraseña — GarageMotor",
                        Body = $@"
                        <div style='font-family: -apple-system, BlinkMacSystemFont, ""Segoe UI"", Roboto, Helvetica, Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 24px; border: 1px solid #1e293b; border-radius: 16px; background-color: #0f172a; color: #f8fafc;'>
                            <div style='text-align: center; margin-bottom: 24px;'>
                                <h2 style='color: #38bdf8; margin: 0; font-size: 24px; font-weight: 800; letter-spacing: -0.5px;'>GarageMotor</h2>
                                <p style='color: #94a3b8; font-size: 13px; margin: 4px 0 0 0;'>Gestión Profesional de Talleres</p>
                            </div>
                            <div style='background-color: #1e293b; border-radius: 12px; padding: 24px; border: 1px solid rgba(255, 255, 255, 0.05);'>
                                <p style='color: #f8fafc; font-size: 16px; line-height: 1.5; margin-top: 0;'>Hola <strong>{user.FullName}</strong>,</p>
                                <p style='color: #cbd5e1; font-size: 15px; line-height: 1.6;'>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en <strong>GarageMotor</strong>.</p>
                                <div style='text-align: center; margin: 32px 0;'>
                                    <a href='{resetUrl}' style='background: linear-gradient(90deg, #0284c7 0%, #2563eb 100%); color: #ffffff; text-decoration: none; padding: 14px 28px; border-radius: 10px; font-weight: 700; font-size: 15px; display: inline-block; box-shadow: 0 4px 14px rgba(2, 132, 199, 0.3);'>Restablecer mi Contraseña</a>
                                </div>
                                <p style='color: #f59e0b; font-size: 13px; line-height: 1.5; margin-bottom: 0;'>⚠️ Este enlace de recuperación expirará en <strong>1 hora</strong> por motivos de seguridad.</p>
                            </div>
                            <p style='color: #64748b; font-size: 13px; line-height: 1.5; margin-top: 20px; text-align: center;'>Si tú no solicitaste este cambio, puedes ignorar este correo de forma segura; tu contraseña permanecerá sin cambios.</p>
                            <hr style='border: 0; border-top: 1px solid #1e293b; margin: 24px 0 16px 0;' />
                            <p style='text-align: center; color: #475569; font-size: 12px; margin: 0;'>GarageMotor PWA — Todos los derechos reservados.</p>
                        </div>"
                    };

                    try
                    {
                        await _emailService.SendEmailAsync(emailRequest, ct);
                    }
                    catch (Exception emailEx)
                    {
                        _logger.LogError(emailEx, "Error al enviar correo SMTP de recuperación para {Email}", user.Email);
                        throw new InvalidOperationException("No se pudo enviar el correo de recuperación. Por favor contacta al administrador para verificar la configuración de correo.");
                    }
                }

                return true;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar token de recuperación para {Email}", email);
                throw new Exception("Ocurrió un error interno al procesar la solicitud de recuperación.");
            }
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto, CancellationToken ct)
        {
            try
            {
                if (dto.NewPassword != dto.ConfirmPassword)
                {
                    throw new ArgumentException("Las contraseñas no coinciden.");
                }

                PasswordResetToken? tokenRecord = await _context.PasswordResetToken
                    .Include(t => t.User)
                    .FirstOrDefaultAsync(t => t.Token == dto.Token && t.IsActive && !t.IsUsed, ct);

                if (tokenRecord == null || tokenRecord.ExpirationDate <= DateTime.Now)
                {
                    return false;
                }

                User user = tokenRecord.User;
                if (user == null) return false;

                // Actualizar contraseña
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                user.MustChangePassword = false;
                user.UpdatedAt = DateTime.Now;

                // Marcar token como utilizado
                tokenRecord.IsUsed = true;
                tokenRecord.UpdatedAt = DateTime.Now;

                _context.User.Update(user);
                _context.PasswordResetToken.Update(tokenRecord);

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al restablecer la contraseña usando el token");
                return false;
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto, CancellationToken ct)
        {
            try
            {
                User? user = await _context.User
                    .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive, ct);

                if (user == null) return false;

                if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
                {
                    throw new UnauthorizedAccessException("La contraseña actual es incorrecta.");
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                user.MustChangePassword = false;
                user.UpdatedAt = DateTime.Now;

                _context.User.Update(user);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar cambio forzado de contraseña para el usuario {UserId}", userId);
                throw;
            }
        }
    }
}
