using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TvMaze.Domain.ShowLinkEntity;

namespace TvMaze.Data.Context
{
    public partial class TvMazeContext : DbContext
    {
        public virtual DbSet<ShowLink> ShowLinks { get; set; }

        public TvMazeContext()
        {
            AttachEventHandlers();
        }

        public TvMazeContext(DbContextOptions<TvMazeContext> options)
            : base(options)
        {
            AttachEventHandlers();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShowLink>(entity =>
            {
                entity.ToTable("ShowLink");

                entity.HasKey(e => e.Id)
                    .HasName("PK_ShowLink");

                entity.Property(e => e.Id);
                entity.Property(e => e.Url);
            });
        }

        partial void OnEntityTrackedPartial(object sender, EntityTrackedEventArgs e);

        partial void OnEntityStateChangedPartial(object sender, EntityStateChangedEventArgs e);
    }
}
