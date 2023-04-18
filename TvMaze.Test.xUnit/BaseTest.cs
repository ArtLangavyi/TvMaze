using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Dynamic;
using System.Reflection;
using System.Text;
using Xunit.Abstractions;

namespace TvMaze.Test.xUnit
{
    public class BaseTest
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;
        protected readonly ITestOutputHelper _testOutputHelper;

        public BaseTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            _server = new TestServer(new WebHostBuilder()
                                    .ConfigureAppConfiguration((context, builder) =>
                                    {
                                        builder.AddJsonFile("appsettings.json");
                                    })
                                    .UseEnvironment("Development")
                                    .UseSerilog());
            _client = _server.CreateClient();
        }

        protected StringContent LoadMockFile(string fileName)
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);

            string json = File.ReadAllText($"{dirPath}/Mock/{fileName}");
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
