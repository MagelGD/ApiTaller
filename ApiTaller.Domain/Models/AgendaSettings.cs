using System;

namespace ApiTaller.Domain.Models
{
    public class AgendaSettings : GeneralEntity
    {
        public int WeeksToOpen { get; set; }
        public int DailySlots { get; set; }
        public TimeSpan BusinessHoursStart { get; set; }
        public TimeSpan BusinessHoursEnd { get; set; }
        public DateTime StartDate { get; set; }
        public string? WorkingDays { get; set; }

        /// <summary>SAAS-2: ID del taller al que pertenece esta configuración.</summary>
        public int WorkshopId { get; set; }
        public virtual Workshop? WorkshopNavigation { get; set; }
    }
}
