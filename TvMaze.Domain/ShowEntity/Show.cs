
using System.ComponentModel.DataAnnotations;

namespace TvMaze.Domain
{
    public class Show : BaseDb
    {
        [StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<Cast> Cast { get; set; }
        public virtual ICollection<ShowCastRelation> ShowCastRelation { get; set; }

        public Show()
        {
            Cast = new HashSet<Cast>();
        }
    }
}
