using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Infra.Data.Context
{
	public class ContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=dracula1!";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)),
              mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
	}
}