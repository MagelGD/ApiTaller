using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderHistory : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string Status { get; set; }
        public string Observations { get; set; }
        public string ActionBy { get; set; } // Nombre de quien realizó el cambio

        public virtual WorkOrder WorkOrderNavigation { get; set; }
    }
}
