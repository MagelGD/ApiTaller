using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Models
{
    public class PaymentMethod : GeneralEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
