using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class Brand : GeneralEntity
    {
        public string Name { get; set; }
        /// <summary>CAR-1: Tipo de vehículo al que pertenece esta marca. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        /// <summary>SAAS-1: ID del taller al que pertenece esta marca</summary>
        public int WorkshopId { get; set; }

    }
}
