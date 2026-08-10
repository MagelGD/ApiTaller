using System;

namespace ApiTaller.Domain.Dtos
{
    public class WorkOrderHistoryDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Status { get; set; } = null!;
        public string Observations { get; set; } = null!;
        public string ActionBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
