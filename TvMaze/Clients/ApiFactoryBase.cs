namespace TvMaze.Workers.Clients
{
    public abstract class ApiFactoryBase : IApiFactoryBase
    {
        private readonly string _clientName;
        private IConfiguration _config { get; init; }
        private readonly IHttpClientFactory _clientFactory;

        protected ApiFactoryBase(string clientName, IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientName = clientName;
            _config = config;
            _clientFactory = clientFactory;
        }

        public HttpClient MakeHttpClient(string? clientName = null)
        {
            return _clientFactory.CreateClient(clientName ?? _clientName);
        }
    }
}
