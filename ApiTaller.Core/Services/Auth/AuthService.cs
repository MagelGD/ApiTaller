using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Login;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Helpers.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiTaller.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtOptions _options;
        private readonly ILoginService _loginService;

        public AuthService(IUserService userService, ILogger<AuthService> logger, IOptions<JwtOptions> options, ILoginService loginService)
        {
            _userService = userService;
            _logger = logger;
            _options = options.Value;
            _loginService = loginService;
        }

        public async Task<IncomeDto> Login(AuthDto auth, CancellationToken cancellation = default)
        {
            try
            {
                LoginUserDto? user = await _userService.GetUser(auth.Username, cancellation);
                if (user is null || BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
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
    }
}
