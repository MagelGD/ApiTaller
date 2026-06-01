using ApiTaller.api.Filters;
using ApiTaller.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTaller.api.Filters
{
    /// <summary>
    /// Filtro global que valida permisos dinámicos contra la tabla RoleAction + Action de la base de datos.
    /// Solo actúa cuando el endpoint tiene el atributo [RequirePermission("slug")].
    /// Sin el atributo, el filtro pasa de largo sin afectar el comportamiento actual.
    /// </summary>
    public class PermissionFilter : IAsyncActionFilter
    {
        private readonly DataContext _context;

        public PermissionFilter(DataContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
        {
            // Si el endpoint NO tiene el atributo → pasa sin restricción extra (comportamiento actual intacto)
            var attr = ctx.ActionDescriptor.EndpointMetadata
                .OfType<RequirePermissionAttribute>()
                .FirstOrDefault();

            if (attr == null)
            {
                await next();
                return;
            }

            // Leer el roleId desde el claim del JWT (ClaimTypes.Role)
            var roleIdClaim = ctx.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (!int.TryParse(roleIdClaim, out int roleId))
            {
                ctx.Result = new ForbidResult();
                return;
            }

            // Consultar la tabla RoleAction usando el Slug exacto de la tabla Action
            // Ejemplo de slugs reales: "Guardar_Usuarios", "Editar_Roles", "Ver_Ordenes_Trabajo"
            bool hasPermission = await _context.RoleAction
                .Include(ra => ra.ActionIdNavigation)
                .AnyAsync(ra => ra.RoleId == roleId
                             && ra.ActionIdNavigation.Slug == attr.Slug
                             && ra.IsActive);

            if (!hasPermission)
            {
                ctx.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
