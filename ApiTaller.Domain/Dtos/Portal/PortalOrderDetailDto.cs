using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalOrderDetailDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleVersion { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public int Mileage { get; set; }
        public string FuelLevel { get; set; }
        public string? Observations { get; set; }
        public string Status { get; set; }
        public string VehicleType { get; set; } = "moto";
        public string VehicleMotorization { get; set; } = "";

        public List<PortalEvidenceDto> Evidences { get; set; } = new();
        public List<PortalPartDto> Parts { get; set; } = new();
        public List<PortalServiceDto> Services { get; set; } = new();
        public List<PortalHistoryDto> History { get; set; } = new();

        public decimal TotalApprovedParts { get; set; }
        public decimal TotalApprovedServices { get; set; }
        public decimal GrandTotalApproved { get; set; }
    }
}
