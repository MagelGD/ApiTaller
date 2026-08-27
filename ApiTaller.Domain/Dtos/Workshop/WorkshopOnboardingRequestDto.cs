using System;

namespace ApiTaller.Domain.Dtos.Workshop
{
    public class WorkshopOnboardingRequestDto
    {
        // Datos del Taller
        public string WorkshopName { get; set; } = string.Empty;
        public string WorkshopType { get; set; } = "moto"; // 'moto' | 'car' | 'multi'
        public string Plan { get; set; } = "basic";
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        // Datos del Administrador/Dueño
        public string AdminFirstName { get; set; } = string.Empty;
        public string AdminFirstSurname { get; set; } = string.Empty;
        public int IdentificationTypeId { get; set; } // ID tipo identificacion
        public string AdminIdentification { get; set; } = string.Empty;
        public string AdminEmail { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;

        // Opciones de Carga Inicial
        public bool SeedProducts { get; set; } = false;
        public bool SeedServices { get; set; } = false;

        // Selección Paramétrica de Módulos (Feature Toggling)
        public List<int>? SelectedModuleIds { get; set; }
    }
}
