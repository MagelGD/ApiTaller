using System;

namespace ApiTaller.Domain.Models
{
    public class SaleDetail : GeneralEntity
    {
        public int SaleId { get; set; }
        public int? ProductId { get; set; }
        public int? ServiceCatalogId { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

        public virtual Sale Sale { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ServiceCatalog Service { get; set; } = null!;
    }
}
