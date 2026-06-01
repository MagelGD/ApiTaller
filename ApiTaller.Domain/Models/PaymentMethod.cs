using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class PaymentMethod : GeneralEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece este método de pago</summary>
        public int WorkshopId { get; set; }
    }
}
