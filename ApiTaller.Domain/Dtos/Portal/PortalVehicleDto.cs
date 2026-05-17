using System;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalVehicleDto
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Color { get; set; }
        public string CylinderCapacity { get; set; }
        public int TotalOrders { get; set; }
        public string? LastOrderStatus { get; set; }
        public DateTime? LastOrderDate { get; set; }
    }
}
