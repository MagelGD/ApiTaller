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

        public async Task<Income> Login(Domain.Dtos.Login.Auth auth, CancellationToken cancellation = default)
        {
            try
            {
                GetUser? user = await _userService.GetUser(auth.Username, cancellation);
                if (user is null || user.Password != auth.Password)
                    return default!;
                user.Token = user.CreateJwt(_options);
                if (string.IsNullOrEmpty(user.Token))
                    return default!;
                if (!await _userService.UpdateUserToken(user, cancellation))
                    return default!;
                if (!await _loginService.AddUserLogin(user, cancellation))
                    return default!;
                Income income = new()
                {
                    Fullname = user.Fullname,
                    Token = user.Token,
                    Success = true,
                    IdUser = user.Id
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
