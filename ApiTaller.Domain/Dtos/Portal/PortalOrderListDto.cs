using System;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalOrderListDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; } = null!;
        public string VehicleBrand { get; set; } = null!;
        public string VehicleVersion { get; set; } = null!;
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal GrandTotal { get; set; }
        public bool HasPendingApproval { get; set; }
        public string VehicleType { get; set; } = "moto";
    }
}
