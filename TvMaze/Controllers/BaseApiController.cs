using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using TvMaze.Core.Models;
using TvMaze.Models.Api;

namespace TvMaze.Controllers
{
    [Route("api/tvmaze-service/v1/[controller]")]
    public partial class BaseApiController : ControllerBase
    {
        protected readonly IConfiguration _config;

        public BaseApiController(IConfiguration config)
        {
            _config = config;
        }

        protected ActionResult OkOrError(ServiceResult response)
        {
            if (response.IsError)
            {
                return Error(response.StatusCode, response.ErrorCode);
            }

            return Ok();
        }


        protected ActionResult<T> ModelOrError<T>(ServiceModelResult<T> response) where T : class, new()
        {
            if (response.IsError || response.Model == null)
            {
                return Error(response);
            }

            return StatusCode((int)response.StatusCode, response.Model);
        }

        protected ActionResult Error(HttpStatusCode statusCode, string errorCode)
        {
            Log.Error("Error response {@statusCode}: {@errorCode}", (int)statusCode, errorCode);

            var errorResponse = new ErrorResponse
            {
                PrimaryErrorCode = errorCode,
                Errors = new List<string> { errorCode }
            };

            return StatusCode((int)statusCode, errorResponse);
        }

        protected ActionResult Error(ServiceResult response)
        {
            return Error(response.StatusCode, response.ErrorCode);
        }
    }
}
