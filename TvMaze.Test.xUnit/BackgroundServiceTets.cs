using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using TvMaze.Workers;

namespace TvMaze.Test.xUnit
{
    public class BackgroundServiceTets
    {
        [Fact]
        public async Task SchedulesIndexingServiceTestRun()
        {
            //Arrange
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IHostedService, SchedulesIndexingService>();
            //mock the dependencies for injection
            services.AddSingleton(Mock.Of<ISchedulesIndexingService>(_ =>
                _.TestConnection(It.IsAny<CancellationToken>()) == Task.CompletedTask
            ));

            var serviceProvider = services.BuildServiceProvider();
            var hostedService = serviceProvider.GetService<IHostedService>();

            //Act
            await hostedService.StartAsync(CancellationToken.None);
            await Task.Delay(1000);//Give some time to invoke the methods under test
            await hostedService.StopAsync(CancellationToken.None);

            //Assert
            var deviceToCloudMessageService = serviceProvider
                .GetRequiredService<ISchedulesIndexingService>();
            //extracting mock to do verifications
            var mock = Mock.Get(deviceToCloudMessageService);
            //assert expected behavior
            mock.Verify(_ => _.TestConnection(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        }
    }
}