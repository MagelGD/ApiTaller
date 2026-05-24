using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class MechanicPaymentSettlement : GeneralEntity
    {
        public int MechanicId { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ServicesCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User MechanicNavigation { get; set; }
        public virtual ICollection<WorkOrderService> Services { get; set; }

        public MechanicPaymentSettlement()
        {
            Services = new HashSet<WorkOrderService>();
        }
    }
}
