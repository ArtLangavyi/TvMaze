namespace TvMaze.Workers.Clients
{
    public interface IApiFactory
    {
        HttpClient MakeHttpClient(string baseUrl, TimeSpan? customTimeout = null);
    }
}
