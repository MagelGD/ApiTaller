using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Services.Auth;
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

        public AuthService(IUserService userService, ILogger<AuthService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> Login(string username, string password, CancellationToken cancellation = default)
        {
            try
            {
                GetUser? user = await _userService.GetUser(username, cancellation);
                if (user != null && user.Password == password)
                {
                    //user.Token = user.CreateJwt(_options);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return default;
        }

        private async Task<string> Token(User user, CancellationToken cancellation = default)
        {
            try
            {
                return "";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }
    }
}
