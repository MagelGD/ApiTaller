using ApiTaller.Domain.Dtos.WorkshopConfig;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.WorkshopSettings
{
    public interface IWorkshopSettingsRepository
    {
        Task<WorkshopSettingsDto?> GetByKeyAsync(string key, CancellationToken cancellation);
        Task<bool> UpsertAsync(WorkshopSettingsDto dto, CancellationToken cancellation);
    }
}
