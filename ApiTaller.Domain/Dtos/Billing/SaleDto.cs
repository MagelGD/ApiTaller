using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Billing
{
    public class SaleDto
    {
        public int Id { get; set; }
        public int? WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public decimal DownPayment { get; set; }
        public decimal Balance { get; set; }
        public string? Observations { get; set; }
        
        // Extended info for invoice
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? VehiclePlate { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleColor { get; set; }

        public List<SaleDetailDto> Details { get; set; }
        public List<SalePaymentDto> Payments { get; set; }
    }
}
