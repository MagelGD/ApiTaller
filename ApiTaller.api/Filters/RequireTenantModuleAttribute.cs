using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaller.api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RequireTenantModuleAttribute : TypeFilterAttribute
    {
        public RequireTenantModuleAttribute(string moduleName) : base(typeof(RequireTenantModuleFilter))
        {
            Arguments = new object[] { moduleName };
        }
    }

    public class RequireTenantModuleFilter : IAsyncActionFilter
    {
        private readonly string _moduleName;
        private readonly ITenantContext _tenantContext;
        private readonly DataContext _dbContext;
        private readonly IMemoryCache _memoryCache;

        public RequireTenantModuleFilter(
            string moduleName,
            ITenantContext tenantContext,
            DataContext dbContext,
            IMemoryCache memoryCache)
        {
            _moduleName = moduleName;
            _tenantContext = tenantContext;
            _dbContext = dbContext;
            _memoryCache = memoryCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 0. Si el endpoint permite acceso anónimo, omitir
            if (context.ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>().Any())
            {
                await next();
                return;
            }

            // 1. Si es SuperAdmin de plataforma, omitir restricción
            if (_tenantContext.IsPlatformAdmin)
            {
                await next();
                return;
            }

            int workshopId = _tenantContext.WorkshopId;
            if (workshopId <= 0)
            {
                context.Result = new ObjectResult(new { message = "No se ha identificado una sede/taller válido." })
                {
                    StatusCode = 403
                };
                return;
            }

            // 2. Consultar o recuperar de caché los módulos activos del taller
            string cacheKey = $"tenant_modules_{workshopId}";
            if (!_memoryCache.TryGetValue(cacheKey, out HashSet<string>? enabledModuleNames) || enabledModuleNames == null)
            {
                var moduleNames = await _dbContext.WorkshopModule
                    .IgnoreQueryFilters()
                    .Where(wm => wm.WorkshopId == workshopId && wm.IsActive)
                    .Include(wm => wm.ModuleNavigation)
                    .Select(wm => wm.ModuleNavigation.Name.Trim())
                    .ToListAsync();

                enabledModuleNames = new HashSet<string>(moduleNames, StringComparer.OrdinalIgnoreCase);
                _memoryCache.Set(cacheKey, enabledModuleNames, TimeSpan.FromMinutes(30));
            }

            // 3. Validar si el módulo requerido está habilitado
            if (!enabledModuleNames.Contains(_moduleName.Trim()))
            {
                context.Result = new ObjectResult(new
                {
                    message = $"El módulo '{_moduleName}' no está habilitado para su taller. Contacte al administrador de la plataforma para activar esta funcionalidad."
                })
                {
                    StatusCode = 403
                };
                return;
            }

            await next();
        }
    }
}
