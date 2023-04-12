using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TvMaze.Domain.Abstractions;

namespace TvMaze.Domain
{
    public class BaseDb : BaseDb<int>
    {
        // empty
    }

    public class BaseDb<IdType> : IBase<IdType>, ITimeTracked
    {
        [Key]
        public IdType Id { get; set; }

        [Column(TypeName = "datetime2(6)")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2(6)")]
        public DateTime UpdatedAt { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "nvarchar(512)")]
        public string? UpdatedBy { get; set; }
    }
}