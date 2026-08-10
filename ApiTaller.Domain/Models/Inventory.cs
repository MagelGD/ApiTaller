using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTaller.Domain.Models
{
    public class Inventory : GeneralEntity
    {
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public int MinStock { get; set; }
        public DateTime LastUpdate { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece este inventario</summary>
        public int WorkshopId { get; set; }

        public virtual Product ProductNavigation { get; set; } = null!;

        public Inventory()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
