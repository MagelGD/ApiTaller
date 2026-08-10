using System;

namespace ApiTaller.Domain.Dtos.WorkOrder
{
    public class WorkOrderPartDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int? ProductId { get; set; }
        public string PartName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsProvidedByCustomer { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public bool IsActive { get; set; }
        public string? ProductName { get; set; } 
        public string? QuotePhotoUrl { get; set; }
        public bool IsApproved { get; set; }
    }
}
