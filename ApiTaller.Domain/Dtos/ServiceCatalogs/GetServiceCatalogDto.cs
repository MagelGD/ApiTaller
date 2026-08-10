using ApiTaller.Domain.Dtos.ServiceTypes;
using System;

namespace ApiTaller.Domain.Dtos.ServiceCatalogs
{
    public class GetServiceCatalogDto
    {
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal DefaultPrice { get; set; }
        public int DefaultMinutes { get; set; }
        public string? TimeUnit { get; set; }
        public string VehicleType { get; set; } = "both";
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetServiceTypeDto? GetServiceType { get; set; }
    }
}
