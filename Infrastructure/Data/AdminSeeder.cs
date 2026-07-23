using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class AdminSeeder
    {
        public static async Task SeederAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0316200103",
                PhoneNumberConfirmed = true,
                Age = 30,
                CreatedAt = DateTime.UtcNow
            };

            // Check if the admin user exists, if not, create it
            var adminUser = await userManager.FindByEmailAsync(admin.Email);
            if (adminUser == null)
            {
                var currentAdmin = await userManager.CreateAsync(admin);
                if (currentAdmin.Succeeded)
                {
                    // Create the Admin role if it doesn't exist
                    var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(admin, "Admin");
                }

            }

        }
    }
}