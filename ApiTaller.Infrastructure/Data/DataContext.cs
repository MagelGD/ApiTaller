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
        public DataContext(DbContextOptions options) : base(options)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
