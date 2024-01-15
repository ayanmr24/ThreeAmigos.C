using ThAmCoSystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace ThAmCoSystem.IdentityDataSeeder
{
    public static class IdentityDataSeeders
    {
        public static async Task SeedAdminUserAndRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ThAmCoSystemUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create the "Admin" role if it doesn't exist
            if (!await roleManager.RoleExistsAsync("Staff"))
            {
                await roleManager.CreateAsync(new IdentityRole("Staff"));
            }

            // Create the admin user if it doesn't exist
            var adminUser = await userManager.FindByNameAsync("ayan@gmail.com");

            // Temporary
            if (adminUser != null)
            {
                await userManager.DeleteAsync(adminUser);
                adminUser = null;
            }




            if (adminUser == null)
            {
                var user = new ThAmCoSystemUser
                {
                    UserName = "ayan@gmail.com",
                    Email = "ayan@gmail.com",
                    EmailConfirmed = true
                };


                var password = "ayan@123";
                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Staff");
                }
            }
        }
    }

}
