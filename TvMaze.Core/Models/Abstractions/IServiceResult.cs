using System.Net;

namespace TvMaze.Core.Models
{
    public interface IServiceResult
    {
        HttpStatusCode StatusCode { get; set; }

        string ErrorCode { get; set; }

        bool IsError { get; }
    }
}
