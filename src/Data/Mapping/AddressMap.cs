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
    public class AddressMap : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(u => u.PostalCode)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(u => u.State)
                .IsRequired()
                .HasMaxLength(35);

            builder.Property(u => u.City)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(u => u.District)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(u => u.Street)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(u => u.AddressNumber)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(u => u.Additional)
                .HasMaxLength(35);

            builder.Property(u => u.Notes)
                .HasMaxLength(150);

            builder.HasOne(user => user.User)
                .WithMany(async => async.Addresses);
        }
    }
}
