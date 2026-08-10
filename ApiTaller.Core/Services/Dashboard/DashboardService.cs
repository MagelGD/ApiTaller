using ApiTaller.Domain.Dtos.Dashboard;
using ApiTaller.Domain.Interfaces.Repositories;
using ApiTaller.Domain.Interfaces.Services;

namespace ApiTaller.Core.Services.Dashboard
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
            int activeOrders = await _dashboardRepository.GetActiveWorkOrdersCountAsync(ct);
            int totalCustomers = await _dashboardRepository.GetTotalCustomersCountAsync(ct);
            int totalVehicles = await _dashboardRepository.GetTotalVehiclesCountAsync(ct);
            decimal operatingAvailability = await _dashboardRepository.GetOperatingAvailabilityPercentAsync(ct);
            IEnumerable<DashboardActivityDto> recentActivities = await _dashboardRepository.GetRecentActivityAsync(6, ct);

            int activeMotoOrders = await _dashboardRepository.GetActiveWorkOrdersCountByTypeAsync("moto", ct);
            int activeCarOrders = await _dashboardRepository.GetActiveWorkOrdersCountByTypeAsync("car", ct);
            int totalMotoVehicles = await _dashboardRepository.GetTotalVehiclesCountByTypeAsync("moto", ct);
            int totalCarVehicles = await _dashboardRepository.GetTotalVehiclesCountByTypeAsync("car", ct);

            return new AdminDashboardStatsDto
            {
                ActiveWorkOrdersCount = activeOrders,
                TotalCustomersCount = totalCustomers,
                TotalVehiclesCount = totalVehicles,
                OperatingAvailabilityPercent = operatingAvailability,
                RecentActivities = recentActivities.ToList(),
                ActiveMotoWorkOrdersCount = activeMotoOrders,
                ActiveCarWorkOrdersCount = activeCarOrders,
                TotalMotoVehiclesCount = totalMotoVehicles,
                TotalCarVehiclesCount = totalCarVehicles
            };
        }
    }
}
