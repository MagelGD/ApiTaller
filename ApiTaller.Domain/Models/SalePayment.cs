using System;

namespace ApiTaller.Domain.Models
{
    public class SalePayment : GeneralEntity
    {
        public int SaleId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string ReferenceCode { get; set; } = null!;
        public DateTime? PaymentDate { get; set; }
        public string? Notes { get; set; }

        public virtual Sale Sale { get; set; } = null!;
        public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    }
}
