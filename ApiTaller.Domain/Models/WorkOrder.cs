using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class WorkOrder : GeneralEntity
    {
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public int Mileage { get; set; }
        public string FuelLevel { get; set; }
        public string Observations { get; set; }
        public string Status { get; set; }
        public decimal DownPayment { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta OT</summary>
        public int WorkshopId { get; set; }

        public virtual Vehicle VehicleNavigation { get; set; }
        public virtual Customer CustomerNavigation { get; set; }
        public virtual ICollection<WorkOrderEvidence> Evidences { get; set; }
        public virtual ICollection<WorkOrderPart> Parts { get; set; }
        public virtual ICollection<WorkOrderService> Services { get; set; }

        public WorkOrder()
        {
            Evidences = new HashSet<WorkOrderEvidence>();
            Parts = new HashSet<WorkOrderPart>();
            Services = new HashSet<WorkOrderService>();
        }
    }
}
