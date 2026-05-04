using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderPart : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public int? ProductId { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsProvidedByCustomer { get; set; }
        public DateTime? WarrantyEndDate { get; set; }

        public virtual WorkOrder WorkOrderNavigation { get; set; }
        public virtual Product ProductNavigation { get; set; }
    }
}
