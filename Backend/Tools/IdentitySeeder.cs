using Microsoft.AspNetCore.Identity;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Tools;

public class IdentitySeeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
{
    public async Task SeedRolesAsync()
    {
        // 1. Ensure roles exist
        string[] roles = ["Admin", "Employee"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Ensure admin user exists
        const string adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                CreatedAt = DateTime.UtcNow
            };

            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}