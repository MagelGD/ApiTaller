using System;

namespace ApiTaller.Domain.Dtos.Inventory
{
    public class InventoryHistoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string MovementType { get; set; }
        public int Quantity { get; set; }
        public int? ReferenceId { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Observations { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ResponsibleUserName { get; set; }
    }
}
