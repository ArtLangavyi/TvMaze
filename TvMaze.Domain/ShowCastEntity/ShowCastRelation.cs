
namespace TvMaze.Domain
{
    public class ShowCastRelation
    {
        public int Id { get; set; }
        public int ShowId { get; internal set; }
        public int CastId { get; internal set; }
        public virtual Show Show { get; set; }
        public virtual Cast Cast { get; set; }

        public ShowCastRelation(int showId, int castId)
        {
            ShowId = showId;
            CastId = castId;
        }
    }
}
