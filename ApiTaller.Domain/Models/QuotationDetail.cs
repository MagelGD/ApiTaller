using System;

namespace ApiTaller.Domain.Models
{
    public class QuotationDetail : GeneralEntity
    {
        public int QuotationId { get; set; }
        public virtual Quotation Quotation { get; set; } = null!;

        // ItemType: "Product" o "Service"
        public string ItemType { get; set; } = "Product";

        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public int? ServiceCatalogId { get; set; }
        public virtual ServiceCatalog? ServiceCatalog { get; set; }

        public string Description { get; set; } = null!;
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        
        public bool IsApproved { get; set; } = true;
    }
}
