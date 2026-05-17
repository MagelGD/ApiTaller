using ApiTaller.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Auth
{
    public interface IPasswordResetTokenRepository
    {
        Task<PasswordResetToken?> GetByTokenAsync(string token, CancellationToken ct);
        Task<bool> AddAsync(PasswordResetToken token, CancellationToken ct);
        Task<bool> UpdateAsync(PasswordResetToken token, CancellationToken ct);
        Task<PasswordResetToken?> GetActiveTokenByUserIdAsync(int userId, CancellationToken ct);
    }
}
