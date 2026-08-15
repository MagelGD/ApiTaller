using System;

namespace ApiTaller.Domain.Dtos.WorkOrder
{
    public class WorkOrderEvidenceDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoBase64 { get; set; }
        public string? EvidenceType { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
