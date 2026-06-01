using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class Supplier : GeneralEntity
    {
        public string DocumentNumber { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece este proveedor</summary>
        public int WorkshopId { get; set; }
    }
}
