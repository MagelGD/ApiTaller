using System;

namespace ApiTaller.Domain.Dtos.WorkshopConfig
{
    public class WorkshopSettingsDto
    {
        public int Id { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ResponsibleUserId { get; set; }
    }
}
