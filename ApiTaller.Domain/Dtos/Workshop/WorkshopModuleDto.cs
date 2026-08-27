using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Workshop
{
    public class WorkshopModuleDto
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> RequiredModuleNames { get; set; } = new List<string>();
    }

    public class UpdateWorkshopModulesDto
    {
        public List<int> ModuleIds { get; set; } = new List<int>();
    }
}
