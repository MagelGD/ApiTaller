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
        public virtual Brand Brand { get; set; }
        public virtual BrandModels Model { get; set; }
    }
}
