using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string dbName)
        {
            //services.AddTransient<CartService>();
            //services.AddTransient<ICartRepository>(x => new CartRepository(dbName));

            return services;
        }
    }
}
