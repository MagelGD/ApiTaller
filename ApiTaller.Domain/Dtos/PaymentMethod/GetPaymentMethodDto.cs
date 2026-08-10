using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Dtos.PaymentMethod
{
    public class GetPaymentMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
