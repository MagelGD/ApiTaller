using System;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalOrderListDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleVersion { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal GrandTotal { get; set; }
        public bool HasPendingApproval { get; set; }
    }
}
