namespace ApiTaller.Domain.Dtos.Dashboard
{
    public class DashboardActivityDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ActionBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
