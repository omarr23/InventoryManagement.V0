using InventoryManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DAL;

public class InventoryDbContext : DbContext
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    // DbSets for all your entities
   public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<InventoryProduct> InventoryProducts { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<Payment> Payments { get; set; }


    //  Fluent API / relationships / constraints
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<InventoryProduct>()
            .HasKey(ip => new { ip.InventoryId, ip.ProductId });

        modelBuilder.Entity<SupplierProduct>()
            .HasKey(sp => new { sp.SupplierId, sp.ProductId });

        // Example: Configure User.Email to be required and unique
        // modelBuilder.Entity<User>()
        //     .HasIndex(u => u.Email)
        //     .IsUnique();

        // modelBuilder.Entity<User>()
        //     .Property(u => u.Email)
        //     .IsRequired();

        // Seed data   here
        // modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "admin", Email = "admin@example.com", PasswordHash = "..." });
    }
}
