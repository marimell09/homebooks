using System;
using Data.Mapping;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infra.Data.Context
{
	public class MyContext : IdentityDbContext
	{
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
              new UserEntity
              {
                  Id = Guid.NewGuid(),
                  Name = "Administrador",
                  Email = "admin@mail.com",
                  CreateAt = DateTime.Now,
                  UpdateAt = DateTime.Now
              }
            );
        }
    }

}
