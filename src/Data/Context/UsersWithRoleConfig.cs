using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class UsersWithRolesConfig : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        private Guid adminUserId = Guid.Parse("B22698B8-42A2-4115-9631-1C2D1E2AC5F7");
        private Guid adminRoleId = Guid.Parse("2301D884-221A-4E7D-B509-0113DCC043E1");

        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            IdentityUserRole<Guid> iur = new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            };

            builder.HasData(iur);
        }
    }
}
