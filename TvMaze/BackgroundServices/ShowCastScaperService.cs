using TvMaze.Services;

namespace TvMaze.Workers
{
    public class ShowCastScaperService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<ShowCastScaperService> _logger;
        private readonly int _delayMinutes;

        private string worker => $"BackgroundService: {nameof(ShowCastScaperService)}";

        public ShowCastScaperService(ILogger<ShowCastScaperService> logger, IConfiguration config, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            _delayMinutes = config.GetValue<int>("AppSettings:SchedulesWorker.BatchDelayMinutesShowIndex");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ShowCastScaperService)} executed: {DateTimeOffset.Now} ", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var scraperScopeService = scope.ServiceProvider.GetService<IScraperScopeService>();

                try
                {
                    await scraperScopeService!.PullDataAsync();

                    _logger.LogInformation($"{worker} sleeping for {_delayMinutes} minutes.");

                    await Task.Delay(_delayMinutes * 60 * 1000, stoppingToken);
                }
                catch (Exception ex) when (!(ex is TaskCanceledException))
                {
                    _logger.LogError(ex, $"{worker} thrown an exception, waiting 3 minutes to continue...");
                    await Task.Delay(3 * 60 * 1000, stoppingToken);
                }
            }

        }
    }
}
