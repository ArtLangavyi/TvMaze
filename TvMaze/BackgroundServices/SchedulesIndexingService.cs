using TvMaze.Services;

namespace TvMaze.Workers
{
    public class SchedulesIndexingService : BackgroundService
    {
        private readonly ILogger<SchedulesIndexingService> _logger;
        private readonly IServiceProvider _services;
        private readonly int _delayMinutes;

        private string worker => $"BackgroundService: {nameof(SchedulesIndexingService)}";

        public SchedulesIndexingService(ILogger<SchedulesIndexingService> logger, IConfiguration config, IServiceProvider services)
        {
            _logger = logger;
            _services = services;

            _delayMinutes = config.GetValue<int>("AppSettings:SchedulesWorker.BatchDelayMinutesSchedulesIndex");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await StartWorker(stoppingToken);
        }

        private async Task StartWorker(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _services.CreateScope())
                {
                    var scraperScopeService = scope.ServiceProvider.GetService<IScraperScopeService>();
                    if (scraperScopeService != null)
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            try
                            {
                                await scraperScopeService.PullAllShowsFromSchedulesAsync();

                                _logger.LogInformation($"{worker} sleeping for {_delayMinutes} minutes.");

                                await Task.Delay(_delayMinutes * 60 * 1000, stoppingToken);
                            }
                            catch (Exception ex) when (!(ex is TaskCanceledException))
                            {
                                _logger.LogError(ex, $"{worker} thrown an exception, waiting 3 minutes to continue...");
                                await Task.Delay(3 * 60 * 1000, stoppingToken);
                            }
                        }
                        _logger.LogInformation($"{worker} cancellation requested.");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{worker} thrown an exception, stopping service...");
            }
            finally
            {
                await StopAsync(stoppingToken);
            }
        }
    }
}
