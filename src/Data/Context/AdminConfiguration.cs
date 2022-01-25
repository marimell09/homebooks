using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;

namespace Data.Context
{

    public class AdminConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {

        private const string adminId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";


        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            string adminEmail = "teste@gmail.com";

            ApplicationUser admin = new ApplicationUser
            {
                Id = adminId,
                FirstName = "Admin",
                LastName = "Last Name",
                UserName = "masteradmin",
                NormalizedUserName = "MASTERADMIN",
                Email = adminEmail.ToString(),
                NormalizedEmail = adminEmail.ToUpper(),
                PhoneNumber = "XXXXXXXXXXXXX",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                DateOfBirth = new DateTime(1980, 1, 1),
                SecurityStamp = new Guid().ToString("D"),
                CreatedAt = null,
                UpdatedAt = null
            };

            admin.PasswordHash = PassGenerate(admin);

            builder.HasData(admin);
        }

        public string PassGenerate(ApplicationUser user)
        {
            string adminPassword = "Senha123!";
            var passHash = new PasswordHasher<ApplicationUser>();
            return passHash.HashPassword(user, adminPassword);
        }
    }
}
