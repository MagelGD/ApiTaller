using System;

namespace ApiTaller.Domain.Dtos
{
    public class WorkOrderHistoryDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public string ActionBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
