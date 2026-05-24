using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class PendingServiceDto
    {
        public int ServiceId { get; set; }
        public int WorkOrderId { get; set; }
        public string Plate { get; set; }
        public string CustomerName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
        public DateTime CompletedAt { get; set; }
        public decimal CommissionAmount { get; set; }
        public string PaymentType { get; set; }
        public decimal ConfiguredValue { get; set; }
    }
}
