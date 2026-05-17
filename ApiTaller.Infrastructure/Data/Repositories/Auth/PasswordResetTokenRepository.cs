using ApiTaller.Domain.Interfaces.Repositories.Auth;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Auth
{
    public class PasswordResetTokenRepository : IPasswordResetTokenRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<PasswordResetTokenRepository> _logger;

        public PasswordResetTokenRepository(DataContext context, ILogger<PasswordResetTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct)
        {
            try
            {
                return await _context.PasswordResetToken
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Token == token && x.IsActive, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener token de restauración de contraseña");
                return null;
            }
        }

        public async Task<bool> AddAsync(PasswordResetToken token, CancellationToken ct)
        {
            try
            {
                await _context.PasswordResetToken.AddAsync(token, ct);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar token de restauración de contraseña");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(PasswordResetToken token, CancellationToken ct)
        {
            try
            {
                _context.PasswordResetToken.Update(token);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar token de restauración de contraseña");
                return false;
            }
        }

        public async Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(int userId, CancellationToken ct)
        {
            try
            {
                return await _context.PasswordResetToken
                    .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive && !x.IsUsed && x.ExpirationDate > DateTime.Now, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener token activo para el usuario {UserId}", userId);
                return null;
            }
        }
    }
}
