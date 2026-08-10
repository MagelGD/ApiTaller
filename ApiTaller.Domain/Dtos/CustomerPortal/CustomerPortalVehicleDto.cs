using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalVehicleDto
    {
        public int Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string CylinderCapacity { get; set; } = null!;
        public string VehicleType { get; set; } = "moto";
        public int TotalOrders { get; set; }
        public string? LastOrderStatus { get; set; }
        public DateTime? LastOrderDate { get; set; }
        public string? ActiveWorkOrderStatus { get; set; }
        public int? ActiveAppointmentId { get; set; }
    }
}
