using Data.Implementations;
using Domain.Interfaces;
using Domain.Repository;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {

        public static void ConfigureDependencyRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //serviceCollection.AddScoped<IUserRepository, UserImplementation>();

   

            serviceCollection.AddDbContext<ApplicationDbContext>(
                options => options.UseMySql(Environment.GetEnvironmentVariable("DB_CONNECTION"),
                 new MySqlServerVersion(new Version(8, 0, 21)),
                     mySqlOptions => mySqlOptions
                    .CharSetBehavior(CharSetBehavior.NeverAppend)
                )
            );
        }
    }
}
