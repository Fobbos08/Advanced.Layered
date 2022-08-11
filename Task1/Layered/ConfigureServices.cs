using Layered.Business;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Task1.Business;
using Task1.Data.CartModule;

namespace Layered
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services, string dbName)
        {
            services.AddTransient<CartService>();
            services.AddTransient<ICartRepository>(x => new CartRepository(dbName));

            services.AddSingleton<MessageHandler>();

            return services;
        }
    }
}
