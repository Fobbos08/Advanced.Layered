using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main (string[] args)
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
                        .AddJsonFile(Path.Combine("configuration",
                                            "configuration.json"))
                        .AddEnvironmentVariables();


                    conf = config.Build();
                })
                .ConfigureServices(s =>
                {
                    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

                    s.AddMvcCore()
                        .AddApiExplorer();

                    s.AddAuthentication("Bearer")
                        .AddJwtBearer("Bearer", options =>
                        {
                            options.Authority = "https://localhost:5001";

                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateAudience = false,
                                RoleClaimType = "role"
                            };
                        });

                    s.AddOcelot()
                        .AddCacheManager(x => x.WithDictionaryHandle());

                    s.AddSwaggerForOcelot(conf, (o) =>
                    {
                        //o.GenerateDocsForAggregates = true;
                        o.GenerateDocsForGatewayItSelf = true;
                    });
                    s.AddSwaggerGen();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .UseIISIntegration()
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseSwagger();

                    //app.UseSwaggerUI();
                    app.UseSwaggerForOcelotUI(opt =>
                    {
                        opt.PathToSwaggerGenerator = "/swagger/docs";
                    }).UseOcelot().Wait();
                })
                .Build()
                .Run();
        }
    }
}
