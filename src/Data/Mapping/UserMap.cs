using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User");
            //Cria chave primária
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email)
                    .IsUnique();

            builder.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(60);

            builder.Property(u => u.Email)
                    .HasMaxLength(100);
        }
    }
}
