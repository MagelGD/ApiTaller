using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Module = ApiTaller.Domain.Models.Module;

namespace ApiTaller.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        private readonly ITenantContext? _tenantContext;

        public DataContext(DbContextOptions options, ITenantContext? tenantContext = null) : base(options)
        {
            _tenantContext = tenantContext;
        }

        // SAAS-0: Raíz del Multi-Tenant
        public DbSet<Workshop> Workshop { get; set; }

        public DbSet<User> User {  get; set; }
        public DbSet<Domain.Models.Action> Action {  get; set; }
        public DbSet<IdentificationType> IdentificationType {  get; set; }
        public DbSet<Inventory> Inventory {  get; set; }
        public DbSet<InventoryHistory> InventoryHistory {  get; set; }
        public DbSet<Login> Login {  get; set; }
        public DbSet<Operation> Operation {  get; set; }
        public DbSet<ProductType> ProductType {  get; set; }
        public DbSet<Product> Product {  get; set; }
        public DbSet<UserRole> UserRole {  get; set; }
        public DbSet<UserRoleModule> UserRoleModule {  get; set; }
        public DbSet<Module> Module {  get; set; }
        public DbSet<RoleAction> RoleAction {  get; set; }
        public DbSet<Brand> Brand {  get; set; }
        public DbSet<BrandModels> BrandModels {  get; set; }
        public DbSet<BrandModelVersion> BrandModelVersion {  get; set; }
        public DbSet<PaymentMethod> PaymentMethod {  get; set; }
        public DbSet<Supplier> Supplier {  get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }
        public DbSet<WorkOrderEvidence> WorkOrderEvidence { get; set; }
        public DbSet<WorkOrderPart> WorkOrderPart { get; set; }
        public DbSet<WorkOrderService> WorkOrderService { get; set; }
        public DbSet<WorkOrderHistory> WorkOrderHistory { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
        public DbSet<ServiceCatalog> ServiceCatalog { get; set; }
        public DbSet<ServicePriceByVersion> ServicePriceByVersion { get; set; }
        public DbSet<InventoryReception> InventoryReception { get; set; }
        public DbSet<InventoryReceptionDetail> InventoryReceptionDetail { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
        public DbSet<SalePayment> SalePayment { get; set; }
        public DbSet<WorkshopSettings> WorkshopSettings { get; set; }
        public DbSet<EmailSettings> EmailSettings { get; set; }
        public DbSet<PasswordResetToken> PasswordResetToken { get; set; }

        // Módulo Agenda
        public DbSet<AgendaSettings> AgendaSettings { get; set; }
        public DbSet<AgendaBlock> AgendaBlock { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<AgendaDayConfig> AgendaDayConfig { get; set; }

        // Módulo Contabilidad (Fase 13)
        public DbSet<MechanicPaymentSettings> MechanicPaymentSettings { get; set; }
        public DbSet<MechanicPaymentSettlement> MechanicPaymentSettlement { get; set; }

        public int CurrentTenantId => _tenantContext?.WorkshopId ?? 0;
        public bool IsPlatformAdmin => _tenantContext?.IsPlatformAdmin ?? false;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // SAAS-1: GLOBAL QUERY FILTERS PARA AISLAMIENTO DE TENANT (WORKSHOP_ID)
            // Evaluado dinámicamente por consulta (EF Core evalúa propiedades de instancia)
            modelBuilder.Entity<UserRole>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<User>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Vehicle>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Brand>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<BrandModels>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<BrandModelVersion>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Product>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<ProductType>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Customer>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<WorkOrder>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<ServiceCatalog>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<ServiceType>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<ServicePriceByVersion>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<PaymentMethod>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Supplier>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Inventory>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<InventoryReception>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Appointment>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<Sale>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<WorkshopSettings>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<MechanicPaymentSettings>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);
            modelBuilder.Entity<MechanicPaymentSettlement>().HasQueryFilter(x => IsPlatformAdmin || x.WorkshopId == CurrentTenantId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
