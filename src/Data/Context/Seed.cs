using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Context
{
    public class Seed {
        public static void SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                IdentityRole role = new IdentityRole { Name = "Admin" };
                roleManager.CreateAsync(role).Wait();
            }

            string email = Environment.GetEnvironmentVariable("AdminEmail").ToString();
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Administrador",
                    Email = email,
                    DateOfBirth = new System.DateTime(1996, 09, 06),
                    UpdatedAt = null
                };

                string password = Environment.GetEnvironmentVariable("AdminPassword").ToString();
                IdentityResult result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
