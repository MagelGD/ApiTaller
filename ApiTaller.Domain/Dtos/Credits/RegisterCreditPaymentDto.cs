using System;

namespace ApiTaller.Domain.Dtos.Credits
{
    public class RegisterCreditPaymentDto
    {
        public int SaleId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public string? ReferenceCode { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Notes { get; set; }
    }
}
