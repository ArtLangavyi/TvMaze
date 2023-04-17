using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TvMaze.Core.Models;
using TvMaze.Helpers;
using TvMaze.Models.Api;

namespace TvMaze.Controllers
{
    [ApiVersion("1")]
    [ResponseCache(CacheProfileName = ResponseCacheHelper.Public5minVaryByAllQueryKeys)]
    public class ShowsController : BaseApiController
    {
        public ShowsController(IConfiguration config) : base(config)
        {
        }

        [HttpGet]
        [OpenApiTag("Shows")]
        [OpenApiOperation("Get Shows", "All shows with cast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<List<int>>> GetShowsWithCastAsync()
        {
            var result = new ServiceModelResult<List<int>>();

            return ModelOrError(result);
        }
    }
}
