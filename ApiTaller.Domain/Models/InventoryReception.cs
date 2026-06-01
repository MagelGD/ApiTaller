using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class InventoryReception : GeneralEntity
    {
        public int? SupplierId { get; set; }
        public DateTime ReceptionDate { get; set; }
        public string InvoiceImageBase64 { get; set; }
        public string Observations { get; set; }
        public decimal TotalAmount { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta recepción de inventario</summary>
        public int WorkshopId { get; set; }

        public virtual Supplier SupplierNavigation { get; set; }
        public virtual ICollection<InventoryReceptionDetail> Details { get; set; } = new List<InventoryReceptionDetail>();
    }
}
