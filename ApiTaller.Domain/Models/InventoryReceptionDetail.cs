using System;

namespace ApiTaller.Domain.Models
{
    public class InventoryReceptionDetail
    {
        public int Id { get; set; }
        public int ReceptionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal SalePrice { get; set; }

        public virtual InventoryReception ReceptionNavigation { get; set; } = null!;
        public virtual Product ProductNavigation { get; set; } = null!;
    }
}
