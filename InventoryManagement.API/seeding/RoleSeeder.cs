using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.DAL.Models;

namespace InventoryManagement.API.Seeding;

public static class RoleSeeder
{
   public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Seed roles
    string[] roles = { "ADMIN", "USER", "MANAGER" }; // Add MANAGER here
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"✅ Role '{role}' created.");
        }
    }

    // Seed admin user (keep this)
    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Password@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "ADMIN");
            Console.WriteLine("✅ Admin user created and assigned to ADMIN role.");
        }
    }
}
}
