namespace TvMaze.Models.Api
{
    public class ErrorResponse
    {
        public string PrimaryErrorCode { get; set; }

        public List<string> Errors { get; set; }
    }
}
