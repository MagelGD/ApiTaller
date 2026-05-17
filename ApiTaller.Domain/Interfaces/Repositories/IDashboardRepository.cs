using ApiTaller.Domain.Dtos.Dashboard;

namespace ApiTaller.Domain.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<int> GetActiveWorkOrdersCountAsync(CancellationToken ct);
        Task<int> GetTotalCustomersCountAsync(CancellationToken ct);
        Task<int> GetTotalVehiclesCountAsync(CancellationToken ct);
        Task<decimal> GetOperatingAvailabilityPercentAsync(CancellationToken ct);
        Task<IEnumerable<DashboardActivityDto>> GetRecentActivityAsync(int limit, CancellationToken ct);
    }
}
