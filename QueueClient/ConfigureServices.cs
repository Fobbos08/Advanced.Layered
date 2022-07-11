using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QueueClient
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<Client>(provider => new Client(configuration.GetValue<string>("HostName")));

            return services;
        }
    }
}
