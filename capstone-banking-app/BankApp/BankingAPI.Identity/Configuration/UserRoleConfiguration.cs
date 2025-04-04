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
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
               new IdentityUserRole<string>
               {
                   RoleId = "41776008 - 6086 - 1fbf - b923 - 2879a6680b9a",
                   UserId = "41776062 - 6086 - 1fbf - b923 - 2879a6680b9a"

               },
            new IdentityUserRole<string>
            {
                RoleId = "41886008 - 6086 - 1fbf - b923 - 2879a6680b9a",
                UserId = "41776062 - 6086 - 1fcf - b923 - 2879a6680b9a"

            }
               );
        }
    }
}
