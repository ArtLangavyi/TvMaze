using System.Text.Json;
using TvMaze.Clients;
using TvMaze.Core.Models.Schedule;
using TvMaze.Core.Services.Shows;
using TvMaze.Workers.Models.Settings;

namespace TvMaze.Services
{
    public class ScraperScopeService : IScraperScopeService
    {
        private readonly ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };
        private ILogger<ScraperScopeService> _logger { get; init; }
        private ITvMazeApiFactory _tvMazeApiFactory { get; init; }
        private TvMazeApiSettings _tvMazeApiSettings { get; init; }
        private IShowService _showService { get; init; }
        private readonly IHttpClientFactory _clientFactory;

        public ScraperScopeService(ILogger<ScraperScopeService> logger, TvMazeApiSettings tvMazeApiSettings, ITvMazeApiFactory tvMazeApiFactory, IShowService showService, IHttpClientFactory clientFactory)
        {
            _tvMazeApiSettings = tvMazeApiSettings;
            _tvMazeApiFactory = tvMazeApiFactory;
            _logger = logger;
            _showService = showService;
            _clientFactory = clientFactory;
        }
        public async Task PullDataAsync()
        {
            var showLinks = await _showService.GetActualShowUrlsAsync();
            if (showLinks!.Count > 0)
            {
                using (var client = _tvMazeApiFactory.MakeHttpClient())
                {
                    await Parallel.ForEachAsync(showLinks, parallelOptions, async (show, cancellationToken) =>
                    {
                        try
                        {
                            var response = await client.GetAsync(show, cancellationToken);

                            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                var sleepTimer = 10;

                                if (response.Headers.Contains("Retry-After"))
                                    sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                                Thread.Sleep(sleepTimer * 1000);
                            }

                            response.EnsureSuccessStatusCode();

                            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Could not get show {show}");
                        }
                    });
                }
            }
        }

        public async Task PullAllShowsFromSchedulesAsync()
        {
            using (var _httpClient = _tvMazeApiFactory.MakeHttpClient())
            {
                try
                {
                    using (var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(_httpClient.Timeout.TotalSeconds * 2)))
                    {
                        try
                        {
                            var response = await _httpClient.GetAsync("/schedule/full", cancellationTokenSource.Token);

                            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                var sleepTimer = 10;

                                if (response.Headers.Contains("Retry-After"))
                                    sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                                Thread.Sleep(sleepTimer * 1000);
                            }

                            response.EnsureSuccessStatusCode();

                            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            var schedules = JsonSerializer.Deserialize<List<ScheduleOverview>>(jsonString);

                            await _showService.SaveShowUrlsAsync(schedules!);

                        }
                        catch (TaskCanceledException ex)
                        {
                            _logger.LogError(ex, $"{PullAllShowsFromSchedulesAsync} TaskCanceledException: {ex.Message} ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Could not deserialize the response body string as {typeof(ScheduleOverview).FullName}.");
                }
            }
        }
    }
}
