using System;

namespace ApiTaller.Domain.Models
{
    public class WorkshopSettings : GeneralEntity
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string? Description { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta configuración</summary>
        public int WorkshopId { get; set; }
        public virtual Workshop WorkshopNavigation { get; set; }
    }
}
