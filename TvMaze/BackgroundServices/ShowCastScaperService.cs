using TvMaze.Services;

namespace TvMaze.Workers
{
    public class ShowCastScaperWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ShowCastScaperWorker> _logger;
        public ShowCastScaperWorker(ILogger<ShowCastScaperWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ShowCastScaperWorker executed: {time} and will take 5 seconds to complete.", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var scraperScopeService = scope.ServiceProvider.GetService<IScraperScopeService>();
                await scraperScopeService!.PullDataAsync();
            }

        }
    }
}
