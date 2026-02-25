using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ApiTaller.Infrastructure.Data
{
    public class DataContext : DbContext
    {
       
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User {  get; set; }
        public DbSet<Domain.Models.Action> Action {  get; set; }
        public DbSet<AplicationModule> AplicationModule {  get; set; }
        public DbSet<IdentificationType> IdentificationType {  get; set; }
        public DbSet<Inventory> Inventory {  get; set; }
        public DbSet<InventoryHistory> InventoryHistory {  get; set; }
        public DbSet<Login> Login {  get; set; }
        public DbSet<Operation> Operation {  get; set; }
        public DbSet<ProductType> ProductType {  get; set; }
        public DbSet<Product> Product {  get; set; }
        public DbSet<UserRole> UserRole {  get; set; }
        public DbSet<UserRoleModule> UserRoleModule {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
