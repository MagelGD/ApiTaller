using System;

namespace ApiTaller.Domain.Dtos.Inventory
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int StockQuantity { get; set; }
        public int MinStock { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? Reference { get; set; }
        public string? Code { get; set; }
        public string? CategoryName { get; set; }
        public string? VehicleType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
