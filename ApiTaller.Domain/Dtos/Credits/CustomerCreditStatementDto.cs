using ApiTaller.Domain.Dtos.Billing;
using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Credits
{
    public class CreditSaleDto
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int? WorkOrderId { get; set; }
        public string? Observations { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public decimal DownPayment { get; set; }
        public decimal Balance { get; set; }
        public List<SalePaymentDto> Payments { get; set; } = new List<SalePaymentDto>();
        public List<SaleDetailDto> Details { get; set; } = new List<SaleDetailDto>();
    }

    public class CustomerCreditStatementDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? IdentificationNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public decimal TotalDebt { get; set; }
        public List<CreditSaleDto> PendingSales { get; set; } = new List<CreditSaleDto>();
    }
}
