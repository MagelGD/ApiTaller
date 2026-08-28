using System;

namespace ApiTaller.Domain.Dtos.Credits
{
    public class CustomerCreditSummaryDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? IdentificationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public decimal TotalDebt { get; set; }
        public int PendingSalesCount { get; set; }
        public DateTime? LastSaleDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }
}
