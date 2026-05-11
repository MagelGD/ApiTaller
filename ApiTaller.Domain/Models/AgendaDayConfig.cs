using System;

namespace ApiTaller.Domain.Models
{
    public class AgendaDayConfig : GeneralEntity
    {
        public DateTime Date { get; set; }
        
        // Si es null, usa el valor global de AgendaSettings
        public int? CustomSlots { get; set; }
        
        public bool IsBlocked { get; set; }
        public string? Reason { get; set; }
    }
}
