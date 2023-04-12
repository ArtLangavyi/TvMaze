namespace TvMaze.Workers.Clients
{
    public class ApiFactory : IApiFactory
    {
        private IConfiguration _config { get; init; }

        public ApiFactory(IConfiguration config)
        {
            _config = config;
        }

        public HttpClient MakeHttpClient(string baseUrl, TimeSpan? customTimeout = null)
        {
            var socketHttpHandler = new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            };

            var httpClient = new HttpClient(socketHttpHandler);
            httpClient.Timeout = new TimeSpan(0, 0, _config.GetValue<int>("AppSettings:HttpClientTimeoutSeconds"));
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestVersion = new Version(2, 0);
            httpClient.DefaultRequestHeaders.ConnectionClose = true;

            if (customTimeout != null)
                httpClient.Timeout = customTimeout.Value;


            return httpClient;
        }
    }
}
