using BankingAPI.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAPI.Identity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser
                {
                    Id = "41776062 - 6086 - 1fbf - b923 - 2879a6680b9a",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    FirstName = "Admin",
                    LastName = "system",
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin@123")

                },
                 new ApplicationUser
                 {
                     Id = "41776062 - 6086 - 1fcf - b923 - 2879a6680b9a",
                     Email = "vani@gmail.com",
                     NormalizedEmail = "VANI@GMAIL.COM",
                     FirstName = "vani",
                     LastName = "shree",
                     UserName = "vani@gmail.com",
                     NormalizedUserName = "VANI@GMAIL.COM",
                     PasswordHash = hasher.HashPassword(null, "Vani@123")
                 }


                );
        }
    }
}
