using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class SalesSummaryDto
    {
        public decimal TotalSales { get; set; }
        public decimal TotalServices { get; set; }
        public decimal TotalParts { get; set; }
        public decimal TotalDownPayments { get; set; }
        public int OrdersCount { get; set; }
    }
}
