using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.BrandModelVersion
{
    public class GetBrandModelVersionDto
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public GetBrandDto brandDto { get; set; }
        public GetBrandModelsDto BrandModelsDto { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
