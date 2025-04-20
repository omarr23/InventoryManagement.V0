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

        // Roles to seed
        string[] roles = ["ADMIN", "USER"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"✅ Role '{role}' created: {result.Succeeded}");
            }
            else
            {
                Console.WriteLine($"ℹ️ Role '{role}' already exists.");
            }
        }

        // Seed default admin user
        string adminEmail = "admin@example.com";
        string adminPassword = "Password@123";

        var existingUser = await userManager.FindByEmailAsync(adminEmail);
        if (existingUser == null)
        {
            var adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            Console.WriteLine($"✅ Admin user created: {result.Succeeded}");

            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(adminUser, "ADMIN");
                Console.WriteLine($"✅ Admin user assigned to 'ADMIN' role: {roleResult.Succeeded}");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"❌ Error creating admin user: {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("ℹ️ Admin user already exists.");
        }
    }
}
