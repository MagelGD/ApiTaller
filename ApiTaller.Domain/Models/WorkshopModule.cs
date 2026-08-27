using System;

namespace ApiTaller.Domain.Models
{
    public class WorkshopModule : GeneralEntity
    {
        public int WorkshopId { get; set; }
        public int ModuleId { get; set; }

        public virtual Workshop WorkshopNavigation { get; set; } = null!;
        public virtual Module ModuleNavigation { get; set; } = null!;
    }
}
