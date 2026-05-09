using ApiTaller.Domain.Dtos.WorkshopConfig;
using ApiTaller.Domain.Interfaces.Repositories.WorkshopSettings;
using ApiTaller.Domain.Interfaces.Services.WorkshopSettings;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.WorkshopSettings
{
    public class WorkshopSettingsService : IWorkshopSettingsService
    {
        private readonly IWorkshopSettingsRepository _repository;

        public WorkshopSettingsService(IWorkshopSettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkshopSettingsDto?> GetByKeyAsync(string key, CancellationToken cancellation)
        {
            return await _repository.GetByKeyAsync(key, cancellation);
        }

        public async Task<bool> UpsertAsync(WorkshopSettingsDto dto, CancellationToken cancellation)
        {
            return await _repository.UpsertAsync(dto, cancellation);
        }
    }
}
