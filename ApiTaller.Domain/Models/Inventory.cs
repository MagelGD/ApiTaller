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

        public virtual Product ProductNavigation { get; set; }

        public Inventory()
        {
            LastUpdate = DateTime.Now;
        }
    }
}
