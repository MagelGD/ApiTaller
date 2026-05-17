using ApiTaller.Domain.Dtos.Dashboard;
using ApiTaller.Domain.Interfaces.Repositories;
using ApiTaller.Domain.Interfaces.Services;

namespace ApiTaller.Application.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<AdminDashboardStatsDto> GetAdminStatsAsync(CancellationToken ct)
        {
            var activeOrders = await _dashboardRepository.GetActiveWorkOrdersCountAsync(ct);
            var totalCustomers = await _dashboardRepository.GetTotalCustomersCountAsync(ct);
            var totalVehicles = await _dashboardRepository.GetTotalVehiclesCountAsync(ct);
            var operatingAvailability = await _dashboardRepository.GetOperatingAvailabilityPercentAsync(ct);
            var recentActivities = await _dashboardRepository.GetRecentActivityAsync(6, ct);

            return new AdminDashboardStatsDto
            {
                ActiveWorkOrdersCount = activeOrders,
                TotalCustomersCount = totalCustomers,
                TotalVehiclesCount = totalVehicles,
                OperatingAvailabilityPercent = operatingAvailability,
                RecentActivities = recentActivities.ToList()
            };
        }
    }
}
