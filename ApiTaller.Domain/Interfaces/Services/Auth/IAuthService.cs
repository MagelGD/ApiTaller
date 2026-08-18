using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Dtos.Auth;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<IncomeDto> Login(Domain.Dtos.Login.AuthDto auth, CancellationToken cancellation = default!);
        Task<LoginResponseDto> LoginAsync(LoginMobileDto credentials, CancellationToken ct);
        Task<bool> GeneratePasswordResetTokenAsync(string email, CancellationToken ct);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto, CancellationToken ct);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto, CancellationToken ct);
        Task<bool> LogoutAsync(int userId, CancellationToken ct = default);
    }
}
