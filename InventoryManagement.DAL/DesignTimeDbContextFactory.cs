using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
{
    var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../InventoryManagement.API");

    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: false)
        .Build();

    var builder = new DbContextOptionsBuilder<InventoryDbContext>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");

    builder.UseSqlServer(connectionString);

    return new InventoryDbContext(builder.Options);
}

    }
} 