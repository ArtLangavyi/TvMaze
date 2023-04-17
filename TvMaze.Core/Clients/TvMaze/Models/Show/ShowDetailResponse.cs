namespace TvMaze.Core.Clients.TvMaze.Models.Show
{
    public class ShowDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ShowEmbededResponse _embedded { get; set; }
    }
}
