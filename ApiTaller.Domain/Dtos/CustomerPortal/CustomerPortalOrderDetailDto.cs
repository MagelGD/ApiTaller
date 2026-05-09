using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalOrderDetailDto
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
        // Solo fotos de tipo "Ingreso"
        public List<string> EntryPhotoUrls { get; set; } = new();
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
