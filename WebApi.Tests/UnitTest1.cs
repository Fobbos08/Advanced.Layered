using System.Net.Http;
using System.Threading.Tasks;
using Business.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace WebApi.Tests
{
    public class Tests
    {
        protected void BeforeTest()
        {
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appSettings.json");
            //var configuration = builder.Build();

            //var services = new ServiceCollection();
            //services.AddInfrastructureServices(configuration);
            //services.AddApplicationServices();
        }

        public Tests()
        {
            // Arrange
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    // ... Configure test services
                });

            var client = application.CreateClient();
            //client.
        }

        //[Fact]
        //public async Task ReturnHelloWorld()
        //{
        //    // Act
        //    var response = await _client.GetAsync("/");
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    // Assert
        //    Assert.Equal("Hello World!", responseString);
        //}

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}