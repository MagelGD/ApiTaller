using System;

namespace ApiTaller.Domain.Models
{
    public class InventoryHistory : GeneralEntity
    {
        public int ProductId { get; set; }
        public string MovementType { get; set; } // Entrada, Salida, Ajuste
        public int Quantity { get; set; }
        public int? ReferenceId { get; set; } // ID de Compra o Orden de Trabajo
        public string Observations { get; set; }

        public virtual Product ProductNavigation { get; set; }
    }
}
