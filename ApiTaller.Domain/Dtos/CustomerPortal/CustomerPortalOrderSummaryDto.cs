using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalOrderSummaryDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; } = null!;
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalParts { get; set; }
        public decimal TotalServices { get; set; }
        public decimal GrandTotal { get; set; }
        public bool HasPendingApproval { get; set; }
    }
}
