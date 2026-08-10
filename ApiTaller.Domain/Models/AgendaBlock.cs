using System;

namespace ApiTaller.Domain.Models
{
    public class AgendaBlock : GeneralEntity
    {
        public DateTime BlockDate { get; set; }
        public string Reason { get; set; } = null!;
    }
}
