using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalVehicleDto
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
