using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderEvidence : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string PhotoUrl { get; set; }
        public string EvidenceType { get; set; }
        public string Description { get; set; }

        public virtual WorkOrder WorkOrderNavigation { get; set; }
    }
}
