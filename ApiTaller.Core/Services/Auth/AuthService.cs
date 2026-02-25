using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;

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
                    user.Token = await Token(user, cancellation);
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
