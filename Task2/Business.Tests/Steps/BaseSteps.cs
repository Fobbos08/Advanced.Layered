using Business.Common.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Business.Tests.Steps
{
    public class BaseSteps
    {
        protected readonly ISender _mediator;

        protected readonly IApplicationDbContext _dbContext;

        protected BaseSteps(ScenarioContext context)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json");
            var configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();

            var provider = services.BuildServiceProvider();

            _mediator = provider.GetService<ISender>();
            _dbContext = provider.GetService<IApplicationDbContext>();
        }
    }
}
