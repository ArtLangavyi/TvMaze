using System.Net;

namespace TvMaze.Core.Models
{
    public class ServiceResult : IServiceResult
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;


        public string ErrorCode { get; set; } = "Undefined";


        public virtual bool IsError
        {
            get
            {
                if (StatusCode < HttpStatusCode.BadRequest)
                {
                    return ErrorCode != "Undefined";
                }

                return true;
            }
        }
    }
}
