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
                ClaimsPrincipal? claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                if (claimsPrincipal == null) return 0;

                string? workshopClaim = claimsPrincipal.FindFirst("workshop_id")?.Value;
                string? roleClaim = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
                
                int id = 0;
                if (int.TryParse(workshopClaim, out id))
                {
                    if (id == 0 || roleClaim == "1")
                    {
                        string? impersonatedWorkshopHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-Workshop-Id"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(impersonatedWorkshopHeader) && int.TryParse(impersonatedWorkshopHeader, out int impersonatedId))
                        {
                            return impersonatedId;
                        }
                        return 0;
                    }

                    return id;
                }

                return 0;
            }
        }

        public string WorkshopType
        {
            get
            {
                ClaimsPrincipal? claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                return claimsPrincipal?.FindFirst("workshop_type")?.Value ?? "moto";
            }
        }

        public bool IsPlatformAdmin
        {
            get
            {
                string? roleClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
                return roleClaim == "1" || WorkshopId == 0;
            }
        }
    }
}
