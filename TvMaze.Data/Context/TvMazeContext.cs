using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TvMaze.Domain;

namespace TvMaze.Data.Context
{
    public partial class TvMazeContext : DbContext
    {
        public virtual DbSet<ShowLink> ShowLinks { get; set; }
        public virtual DbSet<CastPersone> CastPersones { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<ShowCastPersoneRelation> ShowCastPersoneRelation { get; set; }

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

            modelBuilder.Entity<CastPersone>(entity =>
            {
                entity.ToTable("CastPersone");

                entity.HasKey(e => e.Id)
                    .HasName("PK_CastPersone");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Birthday);
            });

            modelBuilder.Entity<ShowCastPersoneRelation>(entity =>
            {
                entity.ToTable("ShowCastPersoneRelation");

                entity.HasKey(e => e.Id)
                    .HasName("PK_ShowCastPersoneRelation");

                entity.Property(e => e.Id);

                entity.HasIndex(e => new { e.Id, e.ShowId, e.CastPersoneId })
                    .HasDatabaseName("IX_ShowCastPersoneRelation_ShowId_CastPersone_id");

                entity.Property(e => e.ShowId);

                entity.Property(e => e.CastPersoneId);

                entity.HasOne(d => d.Show)
                    .WithMany(p => p.ShowCastRelation)
                    .HasForeignKey(d => d.ShowId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ShowCastPersoneRelation_CastPersone_Cascade_Delete");

                entity.HasOne(d => d.CastPersone)
                    .WithMany(p => p.ShowCastRelation)
                    .HasForeignKey(d => d.CastPersoneId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ShowCastPersoneRelation_Show_Cascade_Delete");
            });
        }

        partial void OnEntityTrackedPartial(object sender, EntityTrackedEventArgs e);

        partial void OnEntityStateChangedPartial(object sender, EntityStateChangedEventArgs e);
    }
}
