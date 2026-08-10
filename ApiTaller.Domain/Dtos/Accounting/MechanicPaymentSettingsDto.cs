using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class MechanicPaymentSettingsDto
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }
        public string MechanicName { get; set; } = null!;
        public string PaymentType { get; set; } = null!; // "Porcentaje" o "PorDia"
        public decimal Value { get; set; }
    }
}
