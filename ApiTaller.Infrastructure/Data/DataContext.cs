using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DbSet<User> User {  get; set; }
        public DbSet<Domain.Models.Action> Action {  get; set; }
        public DbSet<AplicationModule> AplicationModule {  get; set; }
        public DbSet<GeneralEntity> GeneralEntity {  get; set; }
        public DbSet<IdentificationType> IdentificationType {  get; set; }
        public DbSet<User> User {  get; set; }
        public DbSet<User> User {  get; set; }
        public DbSet<User> User {  get; set; }
        public DbSet<User> User {  get; set; }
        public DbSet<User> User {  get; set; }
    }
}
