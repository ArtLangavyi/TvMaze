using Microsoft.Extensions.Logging;
using TvMaze.Data.Context;

namespace TvMaze.Core.Services.Shows
{
    public partial class ShowService : BaseService, IShowService
    {
        private readonly TvMazeContext _context;
        private readonly ILogger<ShowService> _logger;

        public ShowService(TvMazeContext context, ILogger<ShowService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
