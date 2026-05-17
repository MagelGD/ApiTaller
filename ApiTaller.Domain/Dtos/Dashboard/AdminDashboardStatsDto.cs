namespace ApiTaller.Domain.Dtos.Dashboard
{
    public class AdminDashboardStatsDto
    {
        public int ActiveWorkOrdersCount { get; set; }
        public int TotalCustomersCount { get; set; }
        public int TotalVehiclesCount { get; set; }
        public decimal OperatingAvailabilityPercent { get; set; }
        public List<DashboardActivityDto> RecentActivities { get; set; } = new List<DashboardActivityDto>();
    }
}
