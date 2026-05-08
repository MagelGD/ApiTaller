using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderService : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string Description { get; set; }
        public int MechanicId { get; set; }
        public decimal Price { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public bool IsApproved { get; set; } = true;

        public virtual WorkOrder WorkOrderNavigation { get; set; }
        public virtual User MechanicNavigation { get; set; }
    }
}
