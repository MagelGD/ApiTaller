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
        public async Task<User?> GetUser(string username, CancellationToken cancellation = default!)
        {
            try
			{
                GetUser? Query = await _context.User.Select(x=> new GetUser { UserName = x.Username, Password = x.Password}).FirstOrDefaultAsync(x=> x.UserName == username, cancellation);
                return Query;
            }
			catch (Exception ex)
			{
                _logger.LogError(ex, Constants.GetUserError);
            }
            return null;
        }
    }
}
