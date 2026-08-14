using System;

namespace ApiTaller.Domain.Models
{
    public class AgendaBlock : GeneralEntity
    {
        public DateTime BlockDate { get; set; }
        public string Reason { get; set; } = null!;

        /// <summary>SAAS-2: ID del taller al que pertenece este bloqueo.</summary>
        public int WorkshopId { get; set; }
        public virtual Workshop? WorkshopNavigation { get; set; }
    }
}
