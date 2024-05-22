using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Xunit;


namespace Magic8Ball.Integration.Tests.Configs
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture> { }


    public class IntegrationTestsFixture : IDisposable
    {
        public TestServer TestServer { get; }

        public HttpClient Client { get; }

        public IntegrationTestsFixture()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.Testing.json");

            var webHostBuilder = new WebHostBuilder()
                .UseKestrel(options => options.Listen(IPAddress.Any, 58835))
                .UseEnvironment("Testing")
                .ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                })
                .UseContentRoot(Directory.GetCurrentDirectory());

            TestServer = new TestServer(webHostBuilder);
            Client = TestServer.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:58835");
        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}
