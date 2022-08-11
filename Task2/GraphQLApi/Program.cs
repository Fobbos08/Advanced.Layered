using System.IO;

using Business;

using Infrastructure;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using QueueClient;

namespace GraphQLApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new WebHostBuilder();
            IConfiguration conf = null;
            builder.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)

                        .AddEnvironmentVariables();

                    conf = config.Build();
                })
                .ConfigureServices(s =>
                {
                    s.AddControllers();
                    s.AddApplicationServices();
                    s.AddInfrastructureServices(conf);
                    s.AddClientServices(conf);
                    s.AddGraphQLServer()
                        .AddQueryType<Query>()
                        .AddMutationType<Mutation>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGraphQL();
                    });
                })
                .Build()
                .Run();
        }
    }
}
