using ApiTaller.Domain.Common.Constants;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Users
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(DataContext dataContext, ILogger<UserRepository> logger)
        {
            _context = dataContext;
            _logger = logger;
        }
        public async Task<GetUser?> GetUser(string username, CancellationToken cancellation = default!)
        {
            try
            {
                GetUser? Query = await _context.User.Select(x => new GetUser
                {
                    Id = x.Id,
                    UserName = x.Username,
                    Password = x.Password,
                    Fullname = x.FullName,
                    Token = x.Token
                }).FirstOrDefaultAsync(x => x.UserName == username, cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return null;
        }

        public async Task<bool> UpdateUserToken(GetUser user, CancellationToken cancellation = default!)
        {
            try
            {
                int rows = await _context.User
                    .Where(x => x.Id == user.Id)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(p => p.Token, user.Token)
                        .SetProperty(p => p.UpdatedAt, DateTime.Now), cancellation);
                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return false;
        }
    }
}
