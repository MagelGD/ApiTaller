using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Inventory
{
    public class InventoryReceptionDto
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime ReceptionDate { get; set; }
        public string Observations { get; set; } = string.Empty;
        public string? InvoiceImageBase64 { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InventoryReceptionDetailDto> Details { get; set; } = new List<InventoryReceptionDetailDto>();
    }
}
