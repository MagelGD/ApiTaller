using ApiTaller.Domain.Dtos.Dashboard;

namespace ApiTaller.Domain.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<AdminDashboardStatsDto> GetAdminStatsAsync(CancellationToken ct);
    }
}
