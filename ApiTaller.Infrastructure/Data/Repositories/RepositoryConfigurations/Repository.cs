using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Repositories.BrandModels;
using ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Repositories.Brands;
using ApiTaller.Domain.Interfaces.Repositories.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Repositories.Login;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Repositories.Operations;
using ApiTaller.Domain.Interfaces.Repositories.PaymentMethods;
using ApiTaller.Domain.Interfaces.Repositories.Products;
using ApiTaller.Domain.Interfaces.Repositories.ProductTypes;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Repositories.Suppliers;
using ApiTaller.Domain.Interfaces.Repositories.Customers;
using ApiTaller.Domain.Interfaces.Repositories.Vehicles;
using ApiTaller.Domain.Interfaces.Repositories.WorkOrders;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Repositories.Billing;
using ApiTaller.Infrastructure.Data.Repositories.Actions;
using ApiTaller.Infrastructure.Data.Repositories.BrandModels;
using ApiTaller.Infrastructure.Data.Repositories.BrandModelVersions;
using ApiTaller.Infrastructure.Data.Repositories.Brands;
using ApiTaller.Infrastructure.Data.Repositories.IdentificationTypes;
using ApiTaller.Infrastructure.Data.Repositories.Login;
using ApiTaller.Infrastructure.Data.Repositories.Modules;
using ApiTaller.Infrastructure.Data.Repositories.Operations;
using ApiTaller.Infrastructure.Data.Repositories.PaymentMethods;
using ApiTaller.Infrastructure.Data.Repositories.Products;
using ApiTaller.Infrastructure.Data.Repositories.ProductTypes;
using ApiTaller.Infrastructure.Data.Repositories.RoleActions;
using ApiTaller.Infrastructure.Data.Repositories.Suppliers;
using ApiTaller.Infrastructure.Data.Repositories.Customers;
using ApiTaller.Infrastructure.Data.Repositories.Vehicles;
using ApiTaller.Infrastructure.Data.Repositories.WorkOrders;
using ApiTaller.Infrastructure.Data.Repositories.UserRoleModules;
using ApiTaller.Infrastructure.Data.Repositories.UserRoles;
using ApiTaller.Infrastructure.Data.Repositories.Users;
using ApiTaller.Infrastructure.Data.Repositories.ServiceTypes;
using ApiTaller.Infrastructure.Data.Repositories.ServiceCatalogs;
using ApiTaller.Infrastructure.Data.Repositories.ServicePrices;
using ApiTaller.Domain.Interfaces.Repositories.ServiceTypes;
using ApiTaller.Domain.Interfaces.Repositories.ServiceCatalogs;
using ApiTaller.Domain.Interfaces.Repositories.ServicePrices;
using ApiTaller.Domain.Interfaces.Repositories.Inventory;
using ApiTaller.Infrastructure.Data.Repositories.Inventory;
using ApiTaller.Infrastructure.Data.Repositories.Billing;
using ApiTaller.Domain.Interfaces.Repositories.WorkshopSettings;
using ApiTaller.Infrastructure.Data.Repositories.WorkshopSettings;
using ApiTaller.Domain.Interfaces.Repositories.CustomerPortal;
using ApiTaller.Infrastructure.Data.Repositories.CustomerPortal;
using ApiTaller.Infrastructure.Data.Repositories.Portal;
using ApiTaller.Domain.Interfaces.Repositories.EmailSettings;
using ApiTaller.Infrastructure.Data.Repositories.EmailSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ApiTaller.Infrastructure.Data.Repositories.RepositoryConfigurations
{
    public static class Repository
    {
        public static IServiceCollection AddRespositories(this IServiceCollection services)
        {
            // SAAS-0: Repository del Tenant
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.Workshop.IWorkshopRepository, ApiTaller.Infrastructure.Data.Repositories.Workshop.WorkshopRepository>();

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
            services.TryAddScoped<IPaymentMethosRepository, PaymentMethodRepository>();
            services.TryAddScoped<ISupplierRepository, SupplierRepository>();
            services.TryAddScoped<ICustomerRepository, CustomerRepository>();
            services.TryAddScoped<IVehicleRepository, VehicleRepository>();
            services.TryAddScoped<IWorkOrderRepository, WorkOrderRepository>();
            services.TryAddScoped<IServiceTypeRepository, ServiceTypeRepository>();
            services.TryAddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
            services.TryAddScoped<IServicePriceByVersionRepository, ServicePriceByVersionRepository>();
            services.TryAddScoped<IInventoryRepository, InventoryRepository>();
            services.TryAddScoped<IInventoryReceptionRepository, InventoryReceptionRepository>();
            services.TryAddScoped<IBillingRepository, BillingRepository>();
            services.TryAddScoped<IWorkshopSettingsRepository, WorkshopSettingsRepository>();
            services.TryAddScoped<ICustomerPortalRepository, CustomerPortalRepository>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.Auth.IPasswordResetTokenRepository, ApiTaller.Infrastructure.Data.Repositories.Auth.PasswordResetTokenRepository>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.Portal.IPortalRepository, PortalRepository>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.Agenda.IAgendaRepository, ApiTaller.Infrastructure.Data.Repositories.Agenda.AgendaRepository>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Services.Email.IEmailService, ApiTaller.Infrastructure.Services.Email.EmailService>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.IDashboardRepository, ApiTaller.Infrastructure.Data.Repositories.Dashboard.DashboardRepository>();
            services.TryAddScoped<IEmailSettingsRepository, EmailSettingsRepository>();
            services.TryAddScoped<ApiTaller.Domain.Interfaces.Repositories.Accounting.IAccountingRepository, ApiTaller.Infrastructure.Data.Repositories.Accounting.AccountingRepository>();
            return services;
        }
    }
}
