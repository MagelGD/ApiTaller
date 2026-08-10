using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Vehicle : GeneralEntity
    {
        public int CustomerId { get; set; }
        public string Plate { get; set; } = null!;
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public string Color { get; set; } = null!;
        public string CylinderCapacity { get; set; } = null!;
        /// <summary>CAR-2: Tipo de vehículo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";
        /// <summary>
        /// SAAS-1: Sub-tipo de vehículo para la categoría 'car'.
        /// Valores: 'sedan' | 'suv' | 'bus' | 'truck'
        /// NULL para vehículos de tipo 'moto'.
        /// </summary>
        public string? VehicleSubType { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece este vehículo</summary>
        public int WorkshopId { get; set; }


        public virtual Customer CustomerNavigation { get; set; } = null!;
        public virtual Brand BrandNavigation { get; set; } = null!;
        public virtual BrandModels ModelNavigation { get; set; } = null!;
        public virtual BrandModelVersion VersionNavigation { get; set; } = null!;
    }
}
