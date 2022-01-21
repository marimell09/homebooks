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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string adminId = "2301D884-221A-4E7D-B509-0113DCC043E1";
        private const string sellerId = "78A7570F-3CE5-48BA-9461-80283ED1D94D";
        private const string customerId = "01B168FE-810B-432D-9010-233BA0B380E9";

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                    new IdentityRole
                    {
                        Id = adminId,
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    },
                    new IdentityRole
                    {
                        Id = sellerId,
                        Name = "Seller",
                        NormalizedName = "SELLER"
                    },
                    new IdentityRole
                    {
                        Id = customerId,
                        Name = "Customer",
                        NormalizedName = "CUSTOMER"
                    }
                );
        }
    }
}
