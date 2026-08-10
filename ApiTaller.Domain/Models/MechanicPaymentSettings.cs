using System;

namespace ApiTaller.Domain.Models
{
    public class MechanicPaymentSettings : GeneralEntity
    {
        public int MechanicId { get; set; }
        public string PaymentType { get; set; } = null!; // "Porcentaje" o "PorDia"
        public decimal Value { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta configuración de pago</summary>
        public int WorkshopId { get; set; }

        public virtual User MechanicNavigation { get; set; } = null!;
    }
}
