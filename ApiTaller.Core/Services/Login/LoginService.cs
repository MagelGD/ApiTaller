using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Login;
using ApiTaller.Domain.Interfaces.Services.Login;
using Microsoft.Extensions.Logging;
using M = ApiTaller.Domain.Models;

namespace ApiTaller.Core.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<LoginService> _logger;
        public LoginService(ILogger<LoginService> logger, ILoginRepository loginRepository)
        {
            _logger = logger;
            _loginRepository = loginRepository;
        }
        public async Task<bool> AddUserLogin(GetUser user, CancellationToken cancellation = default)
        {
            try
            {
                M.Login login = new()
                {
                    Id = 0,
                    UserId = user.Id,
                    Message = $"El usuario {user.UserName} ha iniciado sesión.",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true,
                    ResponsibleUserId = user.Id
                };
                return await _loginRepository.AddUserLogin(login, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }
    }
}
