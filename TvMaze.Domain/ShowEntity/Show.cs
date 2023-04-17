
using System.ComponentModel.DataAnnotations;

namespace TvMaze.Domain
{
    public class Show : BaseDb
    {
        [StringLength(255)]
        public string Name { get; set; }
        public int ShowId { get; set; }
        public virtual ICollection<CastPersone> Cast { get; set; }
        public virtual ICollection<ShowCastPersoneRelation> ShowCastRelation { get; set; }

        public Show()
        {
            Cast = new HashSet<CastPersone>();
        }
    }
}
