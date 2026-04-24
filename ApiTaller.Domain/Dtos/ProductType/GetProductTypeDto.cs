using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.ProductType
{
    public class GetProductTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ResponsibleUser { get; set; }

    }
}
