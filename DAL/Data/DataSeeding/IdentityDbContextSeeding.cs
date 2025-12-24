using DAL.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            try
            {
                #region Roles

                string[] roles = { "SuperAdmin", "Admin" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        var roleResult = await roleManager.CreateAsync(new IdentityRole(role));

                        if (!roleResult.Succeeded)
                        {
                            foreach (var error in roleResult.Errors)
                                Console.WriteLine($"ROLE ERROR: {error.Description}");
                        }
                    }
                }

                #endregion

                #region Super Admin

                var superAdminEmail = "mahmoudmahmoudkarem8@gmail.com";

                var superAdmin = await userManager.FindByEmailAsync(superAdminEmail);

                if (superAdmin == null)
                {
                    superAdmin = new ApplicationUser
                    {
                        FirstName = "Mahmoud",
                        LastName = "Karem",
                        UserName = "mahhmoudkaremm8",
                        Email = superAdminEmail,
                        PhoneNumber = "01554889771",
                        EmailConfirmed = true
                    };

                    var createResult = await userManager.CreateAsync(superAdmin, "AwqNQ#xCh@1");

                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
                    }
                    else
                    {
                        foreach (var error in createResult.Errors)
                            Console.WriteLine($"USER ERROR: {error.Description}");
                    }
                }

                #endregion

                #region Admin

                var adminEmail = "belalkaremm@gmail.com";

                var admin = await userManager.FindByEmailAsync(adminEmail);

                if (admin == null)
                {
                    admin = new ApplicationUser
                    {
                        FirstName = "Belal",
                        LastName = "Karem",
                        UserName = "belalkaremm",
                        Email = adminEmail,
                        PhoneNumber = "01126062449",
                        EmailConfirmed = true
                    };

                    var createResult = await userManager.CreateAsync(admin, "AwqNQ#xCh@0");

                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }
                    else
                    {
                        foreach (var error in createResult.Errors)
                            Console.WriteLine($"USER ERROR: {error.Description}");
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("SEEDING EXCEPTION:");
                Console.WriteLine(ex);
            }
        }
    }
}
