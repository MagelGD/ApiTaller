using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.BrandModels
{
    public class GetBrandModelsDto
    {
        public int Id { get; set; }
        public string Models { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
