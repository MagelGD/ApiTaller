using ApiTaller.Domain.Dtos.WorkshopConfig;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Email
{
    public interface IEmailSettingsService
    {
        Task<EmailSettingsDto?> GetSettingsAsync(CancellationToken ct);
        Task<bool> SaveSettingsAsync(EmailSettingsDto settings, CancellationToken ct);
        Task<bool> TestConnectionAsync(EmailSettingsDto settings, CancellationToken ct);
    }
}
