
namespace TvMaze.Domain
{
    public class Cast : BaseDb
    {
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public virtual ICollection<ShowCastRelation> ShowCastRelation { get; set; }
    }
}
