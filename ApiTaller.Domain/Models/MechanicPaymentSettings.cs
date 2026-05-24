using System;

namespace ApiTaller.Domain.Models
{
    public class MechanicPaymentSettings : GeneralEntity
    {
        public int MechanicId { get; set; }
        public string PaymentType { get; set; } // "Porcentaje" o "PorDia"
        public decimal Value { get; set; }

        public virtual User MechanicNavigation { get; set; }
    }
}
