using ApiTaller.Domain.Interfaces.Repositories.Login;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Infrastructure.Data.Repositories.Login;
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
            return services;
        }
    }
}
