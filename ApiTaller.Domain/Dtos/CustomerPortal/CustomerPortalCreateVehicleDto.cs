using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalCreateVehicleDto
    {
        public string Plate { get; set; } = null!;
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public string Color { get; set; } = null!;
        public string CylinderCapacity { get; set; } = null!;
    }
}
