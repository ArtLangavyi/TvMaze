using System.Net;
using TvMaze.Core.Models;

namespace TvMaze.Core.Services
{
    public class BaseService
    {
        protected ServiceModelResult<T> BadRequest<T>(ServiceModelResult<T> result) where T : class, new()
        {
            if (result == null)
            {
                result = new ServiceModelResult<T>();
            }

            result.ErrorCode = "BadRequest";
            result.StatusCode = HttpStatusCode.BadRequest;
            return result;
        }

        protected ServiceModelResult<T> NotFound<T>(ServiceModelResult<T> result) where T : class, new()
        {
            if (result == null)
            {
                result = new ServiceModelResult<T>();
            }

            result.ErrorCode = "NotFound";
            result.StatusCode = HttpStatusCode.NotFound;
            return result;
        }

        protected ServiceResult NotFound(ServiceResult result)
        {
            if (result == null)
            {
                result = new ServiceResult();
            }

            result.ErrorCode = "NotFound";
            result.StatusCode = HttpStatusCode.NotFound;
            return result;
        }
    }
}
