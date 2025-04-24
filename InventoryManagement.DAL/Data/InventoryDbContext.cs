using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.DAL.Models;

namespace InventoryManagement.DAL;

/// <summary>
/// The main application DbContext that includes both Identity tables and custom domain entities.
/// Inherits from IdentityDbContext to enable ASP.NET Core Identity support.
/// </summary>
public class InventoryDbContext : IdentityDbContext<ApplicationUser>
{
    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
    
    // 👤 Domain-specific tables
  
    public DbSet<Company> Companies { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<InventoryProduct> InventoryProducts { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    /// <summary>
    /// Configure entity relationships, constraints, and seed data.
    /// Also required to call base.OnModelCreating for Identity support.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Composite Keys (keep this)
    modelBuilder.Entity<InventoryProduct>()
        .HasKey(ip => new { ip.InventoryId, ip.ProductId });
    
    modelBuilder.Entity<SupplierProduct>()
        .HasKey(sp => new { sp.SupplierId, sp.ProductId });

    // ❌ Remove all role seeding code here
}
}