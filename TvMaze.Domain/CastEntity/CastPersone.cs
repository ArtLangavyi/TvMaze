
namespace TvMaze.Domain
{
    public class CastPersone : BaseDb
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public virtual ICollection<ShowCastPersoneRelation> ShowCastRelation { get; set; }
    }
}
