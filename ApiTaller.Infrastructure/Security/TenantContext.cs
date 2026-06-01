using ApiTaller.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ApiTaller.Infrastructure.Security
{
    public class TenantContext : ITenantContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int WorkshopId
        {
            get
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                if (claimsPrincipal == null) return 0;

                // Extraemos el claim 'workshop_id' que viaja en el JWT
                var workshopClaim = claimsPrincipal.FindFirst("workshop_id")?.Value;
                var roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
                
                int id = 0;
                if (int.TryParse(workshopClaim, out id))
                {
                    // Si el usuario es SuperAdmin (Rol = 1 o WorkshopId = 0 en BD), le permitimos hacer Impersonation
                    if (id == 0 || roleClaim == "1")
                    {
                        // Intentamos leer el header X-Workshop-Id
                        var impersonatedWorkshopHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Workshop-Id"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(impersonatedWorkshopHeader) && int.TryParse(impersonatedWorkshopHeader, out int impersonatedId))
                        {
                            return impersonatedId; // El SuperAdmin está trabajando dentro de este taller específico
                        }
                        return 0; // El SuperAdmin está en el Dashboard Global (Gestión SaaS)
                    }

                    return id; // Usuario normal, siempre se fuerza su taller real
                }

                return 0; // Si no hay taller, asumimos 0 (puede ser un SuperAdmin)
            }
        }

        public string WorkshopType
        {
            get
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                return claimsPrincipal?.FindFirst("workshop_type")?.Value ?? "moto";
            }
        }

        public bool IsPlatformAdmin
        {
            get
            {
                // Un usuario es PlatformAdmin si su role en los claims lo dice
                // Asumiendo que el Role "SuperAdmin" o "PlatformAdmin" se mapea aquí
                // Por ahora, asumiremos que si WorkshopId es 0, es Admin (puedes ajustar esta lógica)
                var roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                // En ApiTaller, RoleId = 1 suele ser SuperAdmin (ajustar según tu lógica de DB)
                return roleClaim == "1" || WorkshopId == 0;
            }
        }
    }
}
