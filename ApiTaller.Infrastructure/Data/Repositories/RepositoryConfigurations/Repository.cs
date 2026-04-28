using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Repositories.BrandModels;
using ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Repositories.Brands;
using ApiTaller.Domain.Interfaces.Repositories.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Repositories.Login;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Repositories.Operations;
using ApiTaller.Domain.Interfaces.Repositories.Products;
using ApiTaller.Domain.Interfaces.Repositories.ProductTypes;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Infrastructure.Data.Repositories.Actions;
using ApiTaller.Infrastructure.Data.Repositories.BrandModels;
using ApiTaller.Infrastructure.Data.Repositories.BrandModelVersions;
using ApiTaller.Infrastructure.Data.Repositories.Brands;
using ApiTaller.Infrastructure.Data.Repositories.IdentificationTypes;
using ApiTaller.Infrastructure.Data.Repositories.Login;
using ApiTaller.Infrastructure.Data.Repositories.Modules;
using ApiTaller.Infrastructure.Data.Repositories.Operations;
using ApiTaller.Infrastructure.Data.Repositories.Products;
using ApiTaller.Infrastructure.Data.Repositories.ProductTypes;
using ApiTaller.Infrastructure.Data.Repositories.RoleActions;
using ApiTaller.Infrastructure.Data.Repositories.UserRoleModules;
using ApiTaller.Infrastructure.Data.Repositories.UserRoles;
using ApiTaller.Infrastructure.Data.Repositories.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            services.TryAddScoped<IRoleActionsRepository, RoleActionsRepository>();
            services.TryAddScoped<IIdentificationTypesRepository, IdentificationTypesRepository>();
            services.TryAddScoped<IProductRepository, ProductRepository>();
            services.TryAddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.TryAddScoped<IBrandRepository, BrandRepository>();
            services.TryAddScoped<IBrandModelsRepository, BrandModelsRepository>();
            services.TryAddScoped<IBrandModelVersionRepository, BrandModelVersionRepository>();
            return services;
        }
    }
}
