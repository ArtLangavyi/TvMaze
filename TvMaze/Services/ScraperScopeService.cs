using System.Collections.Concurrent;
using System.Text.Json;
using TvMaze.Clients;
using TvMaze.Core.Clients.TvMaze.Models.Schedule;
using TvMaze.Core.Clients.TvMaze.Models.Show;
using TvMaze.Core.Services.Shows;

namespace TvMaze.Services
{
    public class ScraperScopeService : IScraperScopeService
    {
        const int MaxRetries = 5;
        ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 4 };
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        private ILogger<ScraperScopeService> _logger { get; init; }
        private ITvMazeApiFactory _tvMazeApiFactory { get; init; }
        private IShowService _showService { get; init; }

        public ScraperScopeService(ILogger<ScraperScopeService> logger, ITvMazeApiFactory tvMazeApiFactory, IShowService showService, IHttpClientFactory clientFactory)
        {
            _tvMazeApiFactory = tvMazeApiFactory;
            _logger = logger;
            _showService = showService;
        }
        public async Task PullDataAsync()
        {
            var showLinks = await _showService.GetActualShowUrlsAsync();
            if (showLinks!.Count > 0)
            {
                var result = new ConcurrentBag<ShowDetailResponse>();

                using (var _httpClient = _tvMazeApiFactory.MakeHttpClient())
                {
                    await Parallel.ForEachAsync(showLinks, parallelOptions, async (show, cancellationToken) =>
                    {
                        try
                        {
                            var response = await SendAsync(_httpClient, $"{show}?embed=cast", cancellationToken);

                            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            var showCastResponse = JsonSerializer.Deserialize<ShowDetailResponse>(jsonString, jsonSerializerOptions);

                            result.Add(showCastResponse);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Could not get show {show}");
                        }
                    });
                }

                if (result.Count > 0)
                    await _showService.SaveShowWithCastAsync(result.ToList());

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
                            var response = await SendAsync(_httpClient, "/schedule/full", cancellationTokenSource.Token);

                            var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            var schedules = JsonSerializer.Deserialize<List<ScheduleOverviewResponse>>(jsonString, jsonSerializerOptions);

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
                    _logger.LogError(ex, $"Could not deserialize the response body string as {typeof(ScheduleOverviewResponse).FullName}.");
                }
            }
        }

        protected async Task<HttpResponseMessage> SendAsync(HttpClient _httpClient, string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < MaxRetries; i++)
            {
                response = await _httpClient.GetAsync(url, cancellationToken);

                if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                {
                    var sleepTimer = 10;

                    if (response.Headers.Contains("Retry-After"))
                        sleepTimer = int.Parse(response.Headers.GetValues("Retry-After").First());

                    Thread.Sleep(sleepTimer * 1000);

                    await SendAsync(_httpClient, url, cancellationToken);
                }

                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            }

            return response;
        }
    }
}
