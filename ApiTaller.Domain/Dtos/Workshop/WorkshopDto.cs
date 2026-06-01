using System.ComponentModel.DataAnnotations;

namespace ApiTaller.Domain.Dtos.Workshop
{
    // ─── Request DTOs ────────────────────────────────────────────────────────────

    /// <summary>DTO para registrar un nuevo taller (onboarding público)</summary>
    public class RegisterWorkshopDto
    {
        [Required(ErrorMessage = "El nombre del taller es obligatorio.")]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email del propietario es obligatorio.")]
        [EmailAddress]
        public string OwnerEmail { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        /// <summary>
        /// Tipo de negocio (modelo de taller).
        /// Valores válidos: 'moto' | 'car' | 'multi'
        /// </summary>
        [Required(ErrorMessage = "El tipo de taller es obligatorio.")]
        public string WorkshopType { get; set; } = "moto";

        /// <summary>Plan SaaS: 'basic' | 'pro' | 'enterprise'</summary>
        public string Plan { get; set; } = "basic";

        // Credenciales del primer usuario administrador
        [Required]
        [MaxLength(100)]
        public string AdminUsername { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string AdminPassword { get; set; } = string.Empty;
    }

    /// <summary>DTO para actualizar datos generales del taller</summary>
    public class UpdateWorkshopDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        /// <summary>
        /// Solo permite EXPANSIÓN de tipo (moto→multi, car→multi).
        /// La restricción es validada en el servicio.
        /// </summary>
        public string? WorkshopType { get; set; }
    }

    /// <summary>DTO para activar/suspender un taller (solo Platform Admin)</summary>
    public class ToggleWorkshopStatusDto
    {
        public bool IsActive { get; set; }
    }

    // ─── Response DTOs ────────────────────────────────────────────────────────────

    /// <summary>Respuesta de registro exitoso</summary>
    public class RegisterWorkshopResponseDto
    {
        public int WorkshopId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string WorkshopType { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>Datos del taller para listar/ver detalles</summary>
    public class WorkshopDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string WorkshopType { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? TrialEndsAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalUsers { get; set; }
    }

    /// <summary>Resultado de validación de cambio de tipo de taller</summary>
    public class WorkshopTypeChangeValidationDto
    {
        public bool CanChange { get; set; }
        public string CurrentType { get; set; } = string.Empty;
        public string RequestedType { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public int MotorcycleCount { get; set; }
        public int CarCount { get; set; }
    }
}
