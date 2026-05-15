using ApiTaller.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public interface IEmailSettingsService
    {
        Task<EmailSettings> GetSettingsAsync(CancellationToken ct);
        Task<bool> SaveSettingsAsync(EmailSettings settings, CancellationToken ct);
        Task<bool> TestConnectionAsync(EmailSettings settings, CancellationToken ct);
    }
}
