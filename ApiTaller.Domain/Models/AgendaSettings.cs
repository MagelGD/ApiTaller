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
    }
}
