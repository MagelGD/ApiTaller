using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class BrandModels : GeneralEntity
    {
        public string Models { get; set; }
        /// <summary>CAR-1: Tipo de vehículo de este modelo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
    }
}
