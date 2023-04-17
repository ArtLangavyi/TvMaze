
namespace TvMaze.Core.Models.ApiResponse
{
    public class ShowCastOverviewResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShowCastPersoneOverviewResponse> Cast { get; set; }
    }
}
