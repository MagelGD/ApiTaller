using ApiTaller.Core.Services.Actions;
using ApiTaller.Core.Services.Auth;
using ApiTaller.Core.Services.Modules;
using ApiTaller.Core.Services.Operations;
using ApiTaller.Core.Services.RoleActions;
using ApiTaller.Core.Services.UserRoleModules;
using ApiTaller.Core.Services.UserRoles;
using ApiTaller.Core.Services.Users;
using ApiTaller.Domain.Interfaces.Services.Actions;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Login;
using ApiTaller.Domain.Interfaces.Services.Module;
using ApiTaller.Domain.Interfaces.Services.Operations;
using ApiTaller.Domain.Interfaces.Services.RoleActions;
using ApiTaller.Domain.Interfaces.Services.UserRoleModules;
using ApiTaller.Domain.Interfaces.Services.UserRoles;
using ApiTaller.Domain.Interfaces.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.ServiceConfigurations
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.TryAddScoped<ILoginService,Login.LoginService>();
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IAuthService, AuthService>();
            services.TryAddScoped<IUserRoleService, UserRoleService>();
            services.TryAddScoped<IModuleService, ModuleService>();
            services.TryAddScoped<IActionService, ActionService>();
            services.TryAddScoped<IUserRoleModuleService, UserRoleModuleService>();
            services.TryAddScoped<IOperationService, OperationService>();
            services.TryAddScoped<IRoleActionService, RoleActionService>();
            return services;
        }
    }
}
