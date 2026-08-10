using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderEvidence : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string EvidenceType { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual WorkOrder WorkOrderNavigation { get; set; } = null!;
    }
}
