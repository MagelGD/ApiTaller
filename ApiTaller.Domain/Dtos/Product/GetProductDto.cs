using ApiTaller.Domain.Dtos.ProductType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Product
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Code { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetProductTypeDto ProductType { get; set; }
    }
}
