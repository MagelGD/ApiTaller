using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ApiTaller.Infrastructure.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<DataContext> optionsBuilder = new();
            _ = optionsBuilder. UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 32)));

            return new DataContext(optionsBuilder.Options);
        }
    }
}
