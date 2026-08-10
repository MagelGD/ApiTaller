using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderHistory : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string Status { get; set; } = null!;
        public string Observations { get; set; } = null!;
        public string ActionBy { get; set; } = null!; // Nombre de quien realizó el cambio

        public virtual WorkOrder WorkOrderNavigation { get; set; } = null!;
    }
}
