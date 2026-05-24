using System;

namespace ApiTaller.Domain.Dtos.Accounting
{
    public class MechanicSettlementDto
    {
        public int Id { get; set; }
        public int MechanicId { get; set; }
        public string MechanicName { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int ServicesCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
