using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.BrandModels
{
    public class GetBrandModelsDto
    {
        public int Id { get; set; }
        public string Models { get; set; }
        /// <summary>CAR-1: Tipo de vehículo de este modelo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
