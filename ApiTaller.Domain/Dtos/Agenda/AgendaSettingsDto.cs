namespace ApiTaller.Domain.Dtos.Agenda
{
    public class AgendaSettingsDto
    {
        public int Id { get; set; }
        public int WeeksToOpen { get; set; }
        public int DailySlots { get; set; }
        public string BusinessHoursStart { get; set; } = null!;
        public string BusinessHoursEnd { get; set; } = null!;
        public DateTime StartDate { get; set; }
    }
}
