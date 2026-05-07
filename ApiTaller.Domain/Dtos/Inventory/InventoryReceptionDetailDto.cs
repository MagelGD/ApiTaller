namespace ApiTaller.Domain.Dtos.Inventory
{
    public class InventoryReceptionDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal SalePrice { get; set; }
    }
}
