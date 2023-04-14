namespace TvMaze.Workers.Clients
{
    public interface IApiFactoryBase
    {
        HttpClient MakeHttpClient(string? clientName = null);
    }
}
