using ApiTaller.Domain.Common.Constants;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<GetUser?> GetUser(string username, CancellationToken cancellation = default)
        {
            try
            {
                return await _userRepository.GetUser(username, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return null;
        }

        public async Task<bool> UpdateUserToken(GetUser user, CancellationToken cancellation = default)
        {
            try
            {
                return await _userRepository.UpdateUserToken(user, cancellation);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, Constants.GetUserError);
            }
            return false;
        }
    }
}
