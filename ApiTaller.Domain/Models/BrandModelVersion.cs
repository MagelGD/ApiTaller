using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class BrandModelVersion : GeneralEntity
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Version { get; set; }
        /// <summary>CAR-1: Tipo de vehículo de esta referencia. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        /// <summary>SAAS-1: ID del taller al que pertenece esta referencia</summary>
        public int WorkshopId { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual BrandModels Model { get; set; }
    }
}
