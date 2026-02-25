using ApiTaller.Domain.Common.Constants;
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
        public async Task<User> GetUser(string username, CancellationToken cancellation = default!)
        {
            User user = new();
            try
			{
                User? Query = await _context.User.Where(x => x.Username == username).FirstOrDefaultAsync(cancellation);
                return Query ?? user;
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, Constants.GetUserError);
            }
            return user;
        }
    }
}
