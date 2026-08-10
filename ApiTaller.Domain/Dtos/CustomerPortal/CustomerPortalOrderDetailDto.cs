using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalOrderDetailDto
    {
        public int Id { get; set; }
        public string VehiclePlate { get; set; } = null!;
        public string VehicleBrand { get; set; } = null!;
        public string VehicleVersion { get; set; } = null!;
        public string VehicleType { get; set; } = "moto";
        public string VehicleMotorization { get; set; } = "";
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public int Mileage { get; set; }
        public string FuelLevel { get; set; } = null!;
        public string? Observations { get; set; }
        public string Status { get; set; } = null!;
        // Todas las evidencias de la orden
        public List<CustomerPortalEvidenceDto> Evidences { get; set; } = new();
        // Ítems de cotización (sin info interna del mecánico)
        public List<CustomerPortalPartDto> Parts { get; set; } = new();
        public List<CustomerPortalServiceDto> Services { get; set; } = new();
        // Historial de estados (sin exponer ActionBy)
        public List<CustomerPortalHistoryDto> History { get; set; } = new();
        // Totales calculados en el backend
        public decimal TotalApprovedParts { get; set; }
        public decimal TotalApprovedServices { get; set; }
        public decimal GrandTotalApproved { get; set; }
    }
}
