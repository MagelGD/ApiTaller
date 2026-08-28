using ApiTaller.Domain.Dtos.Billing;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Quotations
{
    public class QuotationConvertToSaleDto
    {
        public int QuotationId { get; set; }
        public int? CustomerId { get; set; }
        public decimal DownPayment { get; set; }
        public decimal Balance { get; set; }
        public string? Observations { get; set; }
        public List<SalePaymentDto> Payments { get; set; } = new List<SalePaymentDto>();
    }
}
