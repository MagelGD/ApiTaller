namespace ApiTaller.Domain.Models
{
    /// <summary>
    /// SAAS-0: Entidad raíz del tenant. Cada registro representa una empresa/taller independiente.
    /// workshop_type define el modelo de negocio: 'moto' | 'car' | 'multi'
    /// Una vez que el taller tiene datos operativos, el tipo NO puede restringirse, solo expandirse.
    /// </summary>
    public class Workshop
    {
        public int Id { get; set; }

        /// <summary>Nombre comercial del taller. Ej: "Taller Motos Don Pedro"</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Identificador único de URL amigable. Ej: "taller-don-pedro"</summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>Email del administrador principal del taller</summary>
        public string OwnerEmail { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        /// <summary>
        /// Modelo de negocio del taller. INMUTABLE una vez que hay datos operativos.
        /// 'moto'  = Solo motocicletas
        /// 'car'   = Carros de cualquier tipo (sedan, suv, bus, truck)
        /// 'multi' = Motos + Carros (incluye Lubricentros y Talleres Mixtos)
        /// </summary>
        public string WorkshopType { get; set; } = "moto";

        /// <summary>Plan de suscripción SaaS: 'basic' | 'pro' | 'enterprise'</summary>
        public string Plan { get; set; } = "basic";

        public bool IsActive { get; set; } = true;

        /// <summary>Fecha límite del periodo de prueba gratuita</summary>
        public DateTime? TrialEndsAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navegaciones
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<WorkshopSettings> Settings { get; set; } = new List<WorkshopSettings>();
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
