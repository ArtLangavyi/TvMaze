using System.Text.Json;
using TvMaze.Core.Models.Schedule;
using TvMaze.Core.Services.Shows;
using TvMaze.Workers.Clients;
using TvMaze.Workers.Models.Settings;

namespace TvMaze.Services
{
    public class ScraperScopeService : IScraperScopeService
    {
        private TvMazeApiSettings _tvMazeApiSettings { get; init; }
        private IApiFactory _apiFactory { get; init; }
        private IShowService _showService { get; init; }
        private readonly ILogger<ScraperScopeService> _logger;
        public ScraperScopeService(ILogger<ScraperScopeService> logger, TvMazeApiSettings tvMazeApiSettings, IApiFactory apiFactory, IShowService showService)
        {
            _tvMazeApiSettings = tvMazeApiSettings;
            _apiFactory = apiFactory;
            _logger = logger;
            _showService = showService;
        }
        public Task<bool> PullDataAsync()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            handler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
            handler.EnableMultipleHttp2Connections = true;


            return Task.FromResult(true);
        }

        public async Task PullAllShowsFromSchedulesAsync()
        {
            using (var client = _apiFactory.MakeHttpClient(_tvMazeApiSettings.BaseUrl))
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}/schedule/full")
            {
                Version = new Version(2, 0)
            })
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                try
                {
                    var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var schedules = JsonSerializer.Deserialize<List<ScheduleOverview>>(jsonString);

                    await _showService.SaveShowUrlsAsync(schedules!);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"Could not deserialize the response body string as {typeof(ScheduleOverview).FullName}.");
                }
            }
        }
    }
}
