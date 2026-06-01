using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Models;

namespace ApiTaller.Domain.Interfaces.Repositories.Workshop
{
    public interface IWorkshopRepository
    {
        Task<Domain.Models.Workshop?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Domain.Models.Workshop?> GetBySlugAsync(string slug, CancellationToken ct = default);
        Task<IEnumerable<WorkshopDto>> GetAllAsync(CancellationToken ct = default);
        Task<Domain.Models.Workshop> CreateAsync(Domain.Models.Workshop workshop, CancellationToken ct = default);
        Task<bool> UpdateAsync(Domain.Models.Workshop workshop, CancellationToken ct = default);
        Task<bool> SlugExistsAsync(string slug, CancellationToken ct = default);
        Task<WorkshopTypeChangeValidationDto> ValidateTypeChangeAsync(int workshopId, string newType, CancellationToken ct = default);
    }
}
