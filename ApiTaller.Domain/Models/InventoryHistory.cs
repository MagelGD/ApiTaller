using System;

namespace ApiTaller.Domain.Models
{
    public class InventoryHistory : GeneralEntity
    {
        public int ProductId { get; set; }
        public string MovementType { get; set; } = null!; // Entrada, Salida, Ajuste
        public int Quantity { get; set; }
        public int? ReferenceId { get; set; } // ID de Compra o Orden de Trabajo
        public int? SupplierId { get; set; }
        public string Observations { get; set; } = null!;
        public decimal? UnitCost { get; set; }
        public decimal? SalePrice { get; set; }

        public virtual Product ProductNavigation { get; set; } = null!;
        public virtual Supplier SupplierNavigation { get; set; } = null!;
    }
}
