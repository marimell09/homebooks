using Domain.Interfaces.Address;
using Domain.Interfaces.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAddressService, AddressService>();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddTransient<IClaimsService, ClaimsService>();
            serviceCollection.AddTransient<IRoleService, RoleService>();
        }
    }
}
