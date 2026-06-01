using ApiTaller.Domain.Dtos.Workshop;

namespace ApiTaller.Domain.Interfaces.Services.Workshop
{
    public interface IWorkshopService
    {
        Task<RegisterWorkshopResponseDto> RegisterWorkshopAsync(RegisterWorkshopDto dto, CancellationToken ct = default);
        Task<WorkshopDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<WorkshopDto>> GetAllAsync(CancellationToken ct = default);
        Task<bool> UpdateWorkshopAsync(int id, UpdateWorkshopDto dto, CancellationToken ct = default);
        Task<bool> ToggleStatusAsync(int id, bool isActive, CancellationToken ct = default);
        Task<WorkshopTypeChangeValidationDto> ValidateTypeChangeAsync(int workshopId, string newType, CancellationToken ct = default);
    }
}
