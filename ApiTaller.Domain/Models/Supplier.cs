using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class Supplier : GeneralEntity
    {
        public string DocumentNumber { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string ContactName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        /// <summary>SAAS-1: ID del taller al que pertenece este proveedor</summary>
        public int WorkshopId { get; set; }
    }
}
