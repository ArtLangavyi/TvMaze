using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TvMaze.Domain;

namespace TvMaze.Data.Context
{
    public partial class TvMazeContext : DbContext
    {
        public virtual DbSet<ShowLink> ShowLinks { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<ShowCastRelation> ShowCastRelation { get; set; }

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

            modelBuilder.Entity<Show>(entity =>
            {
                entity.ToTable("Show");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Show");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.ToTable("Cast");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Cast");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Birthday);
            });

            modelBuilder.Entity<ShowCastRelation>(entity =>
            {
                entity.ToTable("ShowCastRelation");

                entity.HasKey(e => e.Id)
                    .HasName("PK_ShowCastRelation");

                entity.Property(e => e.Id);

                entity.HasIndex(e => new { e.Id, e.ShowId, e.CastId})
                    .HasDatabaseName("IX_ShowCastRelation_ShowId_Cast_id");

                entity.Property(e => e.ShowId);

                entity.Property(e => e.CastId);

                entity.HasOne(d => d.Cast)
                    .WithMany(p => p.ShowCastRelation)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ShowCastRelation_Cast_Cascade");

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.ShowCastRelation)
                    .HasForeignKey(d => d.CastId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ShowCastRelation_Show_Cascade");
            });
        }

        partial void OnEntityTrackedPartial(object sender, EntityTrackedEventArgs e);

        partial void OnEntityStateChangedPartial(object sender, EntityStateChangedEventArgs e);
    }
}
