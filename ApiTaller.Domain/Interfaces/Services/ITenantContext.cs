namespace ApiTaller.Domain.Interfaces.Services
{
    /// <summary>
    /// SAAS-1: Contrato para el contexto del tenant actual.
    /// Permite que cualquier capa del sistema conozca qué taller está operando
    /// sin necesidad de pasarlo manualmente en cada método.
    /// </summary>
    public interface ITenantContext
    {
        /// <summary>
        /// ID del taller del usuario autenticado.
        /// Retorna 0 si es un Super Admin de la plataforma (IsPlatformAdmin = true).
        /// </summary>
        int WorkshopId { get; }

        /// <summary>
        /// Tipo de negocio del taller actual: 'moto' | 'car' | 'multi'
        /// </summary>
        string WorkshopType { get; }

        /// <summary>
        /// True si el usuario es Super Admin de la plataforma SaaS.
        /// Puede ver datos de TODOS los talleres.
        /// </summary>
        bool IsPlatformAdmin { get; }
    }
}
