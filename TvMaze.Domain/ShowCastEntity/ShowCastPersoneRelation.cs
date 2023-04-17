
namespace TvMaze.Domain
{
    public class ShowCastPersoneRelation
    {
        public int Id { get; set; }
        public int ShowId { get; internal set; }
        public int CastPersoneId { get; internal set; }
        public virtual Show Show { get; set; }
        public virtual CastPersone CastPersone { get; set; }

        public ShowCastPersoneRelation(int showId, int castPersoneId)
        {
            ShowId = showId;
            CastPersoneId = castPersoneId;
        }
    }
}
