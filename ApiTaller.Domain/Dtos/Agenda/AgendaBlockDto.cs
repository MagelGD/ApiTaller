using System;

namespace ApiTaller.Domain.Dtos.Agenda
{
    public class AgendaBlockDto
    {
        public int Id { get; set; }
        public DateTime BlockDate { get; set; }
        public string Reason { get; set; } = null!;
    }
}
