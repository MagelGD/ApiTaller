using ApiTaller.Domain.Common.Constants;
using ApiTaller.Domain.Interfaces.Repositories.Login;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<LoginRepository> _logger;
        public LoginRepository(DataContext dataContext, ILogger<LoginRepository> logger)
        {
            _context = dataContext;
            _logger = logger;
        }
        public async Task<bool> AddUserLogin(Domain.Models.Login login, CancellationToken cancellation = default)
        {
            try
            {
                await _context.Login.AddAsync(login, cancellation);
                if(await _context.SaveChangesAsync(cancellation) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return false;
        }
    }
}
