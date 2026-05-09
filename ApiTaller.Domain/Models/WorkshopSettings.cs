using System;

namespace ApiTaller.Domain.Models
{
    public class WorkshopSettings : GeneralEntity
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string? Description { get; set; }
    }
}
