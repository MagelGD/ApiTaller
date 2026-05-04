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
        public string Plate { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public string Color { get; set; }
        public string CylinderCapacity { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetBrandDto? Brand { get; set; }
        public GetBrandModelsDto? Model { get; set; }
        public GetBrandModelVersionDto? Reference { get; set; }
    }
}
