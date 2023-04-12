using System.Text.Json;

using TvMaze.Workers.Clients;
using TvMaze.Workers.Models.Settings;

namespace TvMaze.Services
{
    public class ScraperScopeService : IScraperScopeService
    {
        private TvMazeApiSettings _tvMazeApiSettings { get; init; }
        private IApiFactory _apiFactory { get; init; }
        private readonly ILogger<ScraperScopeService> _logger;
        public ScraperScopeService(ILogger<ScraperScopeService> logger, TvMazeApiSettings tvMazeApiSettings, IApiFactory apiFactory)
        {
            _tvMazeApiSettings = tvMazeApiSettings;
            _apiFactory = apiFactory;
            _logger = logger;
        }
        public Task<bool> PullDataAsync()
        {
            return Task.FromResult(true);
        }

        public async Task PullAllShowsFromSchedulesAsync()
        {
            // https://assets.ctfassets.net/9n3x4rtjlya6/2D1uCQKQ4QoaE4YCEq4sey/0467c6c44911a6bc6530793307b8b7fc/Evgeny_Zhirov_-_Microservice_interaction_with_HTTP2.pdf
            // https://makolyte.com/csharp-configuring-how-long-an-httpclient-connection-will-stay-open/

            // https://stackoverflow.com/questions/21611674/how-to-auto-generate-a-c-sharp-class-file-from-a-json-string

            using (var client = _apiFactory.MakeHttpClient(_tvMazeApiSettings.BaseUrl))
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}/schedule/full")
            {
                Version = new Version(2, 0)
            })
            {

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = JsonSerializer.Deserialize<ScheduleOverview[]>(jsonString);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, $"Could not deserialize the response body string as {typeof(ScheduleOverview).FullName}.");
                }
            }
        }
    }
}
