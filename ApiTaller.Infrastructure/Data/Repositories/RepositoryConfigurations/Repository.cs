using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Repositories.Login;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Repositories.Operations;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Infrastructure.Data.Repositories.Actions;
using ApiTaller.Infrastructure.Data.Repositories.Login;
using ApiTaller.Infrastructure.Data.Repositories.Modules;
using ApiTaller.Infrastructure.Data.Repositories.Operations;
using ApiTaller.Infrastructure.Data.Repositories.UserRoleModules;
using ApiTaller.Infrastructure.Data.Repositories.UserRoles;
using ApiTaller.Infrastructure.Data.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.RepositoryConfigurations
{
    public static class Repository
    {
        public static IServiceCollection AddRespositories(this IServiceCollection services)
        {
            services.TryAddScoped<ILoginRepository, LoginRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IUserRoleRepository, UserRoleRepository>();
            services.TryAddScoped<IModuleRepository, ModuleRepository>();
            services.TryAddScoped<IActionRepository, ActionRepository>();
            services.TryAddScoped<IUserRoleModuleRepository, UserRoleModuleRepository>();
            services.TryAddScoped<IOperationRepository, OperationRepository>();
            return services;
        }
    }
}
