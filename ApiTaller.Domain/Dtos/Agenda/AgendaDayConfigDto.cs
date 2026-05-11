using System;

namespace ApiTaller.Domain.Dtos.Agenda
{
    public class AgendaDayConfigDto
    {
        public DateTime Date { get; set; }
        public int? CustomSlots { get; set; }
        public bool IsBlocked { get; set; }
        public string? Reason { get; set; }
        public int CurrentBookings { get; set; } // Informativo para el admin
    }
}
