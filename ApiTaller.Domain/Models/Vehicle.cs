using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Vehicle : GeneralEntity
    {
        public int CustomerId { get; set; }
        public string Plate { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int? VersionId { get; set; }
        public string Color { get; set; }
        public string CylinderCapacity { get; set; }
        /// <summary>CAR-2: Tipo de vehículo. 'moto' | 'car'</summary>
        public string VehicleType { get; set; } = "moto";

        public virtual Customer CustomerNavigation { get; set; }
        public virtual Brand BrandNavigation { get; set; }
        public virtual BrandModels ModelNavigation { get; set; }
        public virtual BrandModelVersion VersionNavigation { get; set; }
    }
}
