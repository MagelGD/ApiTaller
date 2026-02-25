using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Helpers.Jwt
{
    public class GenerateToken
    {
        public GenerateToken()
        {
            
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
