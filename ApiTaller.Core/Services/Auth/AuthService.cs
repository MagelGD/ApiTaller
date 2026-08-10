using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Dtos.Auth;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Login;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Helpers.Jwt;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public AuthService(
            IUserService userService, 
            ILogger<AuthService> logger, 
            IOptions<JwtOptions> options, 
            ILoginService loginService,
            DataContext context,
            IEmailService emailService)
        {
            _userService = userService;
            _logger = logger;
            _options = options.Value;
            _loginService = loginService;
            _context = context;
            _emailService = emailService;
        }

        public async Task<IncomeDto> Login(AuthDto auth, CancellationToken cancellation = default)
        {
            try
            {
                LoginUserDto? user = await _userService.GetUser(auth.Username, cancellation);
                string dato = BCrypt.Net.BCrypt.HashPassword(auth.Password);
                if (user is null || !BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
                    return default!;
                //if (user is null || user.Password != auth.Password)
                //    return default!;
                user.Token = user.CreateJwt(_options);
                user.ExpireToken = _options.AccessTokenMinutes;
                if (string.IsNullOrEmpty(user.Token))
                    return default!;
                if (!await _userService.UpdateUserToken(user, cancellation))
                    return default!;
                if (!await _loginService.AddUserLogin(user, cancellation))
                    return default!;
                IncomeDto income = new()
                {
                    Fullname = user.Fullname,
                    Token = user.Token,
                    Success = true,
                    IdUser = user.Id,
                    IdRoleUser = user.IdUserRole ?? 0
                };
                return income;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
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

                string token = user.CreateJwt(customer?.Id, _options);

                return new LoginResponseDto
                {
                    Token = token,
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

        public async Task<bool> GeneratePasswordResetTokenAsync(string email, CancellationToken ct)
        {
            try
            {
                User? user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.IsActive, ct);

                if (user == null)
                {
                    // Retornamos true por seguridad para evitar enumeración de cuentas
                    return true;
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
                    // Enviar correo de recuperación con plantilla premium
                    string resetUrl = $"http://localhost:4200/reset-password?token={tokenString}";
                    EmailRequest emailRequest = new EmailRequest
                    {
                        To = user.Email,
                        Subject = "Recupera tu contraseña — Deivid Motos",
                        Body = $@"
                        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px; background-color: #ffffff;'>
                            <div style='text-align: center; margin-bottom: 20px;'>
                                <h2 style='color: #0ea5e9; margin: 0;'>Deivid Motos</h2>
                                <p style='color: #6b7280; font-size: 14px; margin: 5px 0 0 0;'>Portal Cliente</p>
                            </div>
                            <hr style='border: 0; border-top: 1px solid #e0e0e0; margin-bottom: 20px;' />
                            <p style='color: #374151; font-size: 16px; line-height: 1.5;'>Hola <strong>{user.FullName}</strong>,</p>
                            <p style='color: #374151; font-size: 16px; line-height: 1.5;'>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en el portal de Deivid Motos.</p>
                            <div style='text-align: center; margin: 30px 0;'>
                                <a href='{resetUrl}' style='background-color: #0ea5e9; color: #ffffff; text-decoration: none; padding: 12px 24px; border-radius: 6px; font-weight: bold; font-size: 16px; display: inline-block;'>Restablecer mi Contraseña</a>
                            </div>
                            <p style='color: #ef4444; font-size: 14px; line-height: 1.5;'>Este enlace de recuperación expirará en 1 hora por razones de seguridad.</p>
                            <p style='color: #6b7280; font-size: 14px; line-height: 1.5; margin-top: 20px;'>Si tú no solicitaste este cambio, puedes ignorar este correo de forma segura; tu contraseña seguirá siendo la misma.</p>
                            <hr style='border: 0; border-top: 1px solid #e0e0e0; margin: 25px 0 15px 0;' />
                            <p style='text-align: center; color: #9ca3af; font-size: 12px; margin: 0;'>Deivid Motos PWA — Todos los derechos reservados.</p>
                        </div>"
                    };

                    await _emailService.SendEmailAsync(emailRequest, ct);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar token de recuperación para {Email}", email);
                return false;
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
