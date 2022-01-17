using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Infra.Data.Context
{
	public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
	{
		public MyContext CreateDbContext(string[] args)
		{
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=dracula1!";

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)),
              mySqlOptions => mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)
            );

            return new MyContext(optionsBuilder.Options);
        }
	}
}