using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.Supplier
{
    public class GetSupplierDto
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
