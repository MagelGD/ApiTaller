using System;

namespace ApiTaller.Domain.Dtos.WorkOrder
{
    public class WorkOrderEvidenceDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string EvidenceType { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
