using System;

namespace ApiTaller.Domain.Models
{
    public class ProductComboItem : GeneralEntity
    {
        public int ParentProductId { get; set; }
        public int ChildProductId { get; set; }
        public int Quantity { get; set; }
        public int WorkshopId { get; set; }

        public virtual Product ParentProduct { get; set; } = null!;
        public virtual Product ChildProduct { get; set; } = null!;
    }
}
