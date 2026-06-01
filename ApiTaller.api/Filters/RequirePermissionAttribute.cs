using System;

namespace ApiTaller.api.Filters
{
    /// <summary>
    /// Decorador para requerir un permiso específico en un endpoint.
    /// El slug debe coincidir exactamente con el campo Action.Slug de la base de datos.
    /// Ejemplo: [RequirePermission("Guardar_Usuarios")]
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class RequirePermissionAttribute : Attribute
    {
        public string Slug { get; }

        public RequirePermissionAttribute(string slug)
        {
            Slug = slug;
        }
    }
}
