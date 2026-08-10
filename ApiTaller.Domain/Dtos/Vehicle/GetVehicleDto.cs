using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Dtos.BrandModelVersion;
using System;

namespace ApiTaller.Domain.Dtos.Vehicle
{
    public class GetVehicleDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Plate { get; set; } = null!;
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public string Color { get; set; } = null!;
        public string CylinderCapacity { get; set; } = null!;
        /// <summary>CAR-2: Tipo de vehículo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetBrandDto? Brand { get; set; }
        public GetBrandModelsDto? Model { get; set; }
        public GetBrandModelVersionDto? Reference { get; set; }
    }
}
