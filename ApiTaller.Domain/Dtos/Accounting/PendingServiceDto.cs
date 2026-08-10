using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class PendingServiceDto
    {
        public int ServiceId { get; set; }
        public int WorkOrderId { get; set; }
        public string Plate { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public decimal ServicePrice { get; set; }
        public DateTime CompletedAt { get; set; }
        public decimal CommissionAmount { get; set; }
        public string PaymentType { get; set; } = null!;
        public decimal ConfiguredValue { get; set; }
    }
}
