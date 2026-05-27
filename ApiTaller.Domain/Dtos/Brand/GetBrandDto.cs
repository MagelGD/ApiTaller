using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Brand
{
    public class GetBrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>CAR-1: Tipo de vehículo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
