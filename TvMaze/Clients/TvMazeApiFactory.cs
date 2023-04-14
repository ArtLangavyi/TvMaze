using TvMaze.Workers.Clients;

namespace TvMaze.Clients
{
    public interface ITvMazeApiFactory : IApiFactoryBase
    {

    }

    public class TvMazeApiFactory : ApiFactoryBase, ITvMazeApiFactory
    {
        public TvMazeApiFactory(IHttpClientFactory clientFactory, IConfiguration config) : base("tvmaze-api", clientFactory, config)
        {

        }
    }
}
