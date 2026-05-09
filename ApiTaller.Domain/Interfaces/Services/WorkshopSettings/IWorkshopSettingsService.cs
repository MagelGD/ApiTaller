using ApiTaller.Domain.Dtos.WorkshopConfig;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.WorkshopSettings
{
    public interface IWorkshopSettingsService
    {
        Task<WorkshopSettingsDto?> GetByKeyAsync(string key, CancellationToken cancellation);
        Task<bool> UpsertAsync(WorkshopSettingsDto dto, CancellationToken cancellation);
    }
}
