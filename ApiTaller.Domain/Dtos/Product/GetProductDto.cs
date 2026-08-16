using ApiTaller.Domain.Dtos.ProductType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string Code { get; set; } = null!;
        public string Reference { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string VehicleType { get; set; } = "both";
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetProductTypeDto ProductType { get; set; } = null!;
    }
}
