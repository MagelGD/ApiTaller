using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Sale : GeneralEntity
    {
        public int? WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        public decimal DownPayment { get; set; }
        public decimal Balance { get; set; }
        public string Observations { get; set; }
        public string? WorkshopName { get; set; }
        public string? WorkshopSlogan { get; set; }
        public string? LogoBase64 { get; set; }
        public string? LogoBrandsBase64 { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta venta</summary>
        public int WorkshopId { get; set; }

        public virtual WorkOrder WorkOrder { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleDetail> Details { get; set; }
        public virtual ICollection<SalePayment> Payments { get; set; }
    }
}
