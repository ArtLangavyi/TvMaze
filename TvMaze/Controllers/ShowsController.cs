using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TvMaze.Core.Models.ApiResponse;
using TvMaze.Core.Services.Shows;
using TvMaze.Helpers;

namespace TvMaze.Controllers
{
    [ApiVersion("1")]
    [ResponseCache(CacheProfileName = ResponseCacheHelper.Public5minVaryByAllQueryKeys)]
    public class ShowsController : BaseApiController
    {
        private readonly IShowService _showService;
        public ShowsController(IConfiguration config, IShowService showService) : base(config)
        {
            _showService = showService;
        }

        [HttpGet("overview")]
        [OpenApiTag("Shows")]
        [OpenApiOperation("Get Shows", "All shows with cast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ShowCastOverviewResponse>>> GetShowsWithCastAsync()
        {
            var result = await _showService.GetShowsWithCastAsync();

            return ModelOrError(result);
        }
    }
}
