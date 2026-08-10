using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Supplier
{
    public class GetSupplierDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
