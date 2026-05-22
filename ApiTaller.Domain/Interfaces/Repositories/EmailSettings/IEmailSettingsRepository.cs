using ApiTaller.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.EmailSettings
{
    public interface IEmailSettingsRepository
    {
        Task<Domain.Models.EmailSettings?> GetSettingsAsync(CancellationToken cancellation);
        Task<bool> SaveSettingsAsync(Domain.Models.EmailSettings settings, CancellationToken cancellation);
    }
}
