using System;

namespace ApiTaller.Domain.Models
{
    public class WorkOrderService : GeneralEntity
    {
        public int WorkOrderId { get; set; }
        public string Description { get; set; }
        public int MechanicId { get; set; }
        public decimal Price { get; set; }
        public int EstimatedMinutes { get; set; }
        public string TimeUnit { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public bool IsApproved { get; set; } = true;
        
        public bool IsPaidToMechanic { get; set; } = false;
        public DateTime? PaidToMechanicAt { get; set; }
        public int? MechanicPaymentSettlementId { get; set; }

        public virtual WorkOrder WorkOrderNavigation { get; set; }
        public virtual User MechanicNavigation { get; set; }
        public virtual MechanicPaymentSettlement MechanicPaymentSettlementNavigation { get; set; }
    }
}
