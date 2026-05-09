using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalOrderSummaryDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal TotalParts { get; set; }
        public decimal TotalServices { get; set; }
        public decimal GrandTotal { get; set; }
        public bool HasPendingApproval { get; set; }
    }
}
