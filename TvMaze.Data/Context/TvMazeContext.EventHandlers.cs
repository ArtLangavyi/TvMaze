using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TvMaze.Domain.Abstractions;

namespace TvMaze.Data.Context
{
    public partial class TvMazeContext
    {
        protected void AttachEventHandlers()
        {
            ChangeTracker.Tracked -= OnEntityTrackedPartial;
            ChangeTracker.Tracked += OnEntityTrackedPartial;

            ChangeTracker.StateChanged -= OnEntityStateChangedPartial;
            ChangeTracker.StateChanged += OnEntityStateChangedPartial;
        }

        partial void OnEntityTrackedPartial(object sender, EntityTrackedEventArgs e)
        {
            if (e.FromQuery)
                return;

            if (e.Entry.State == EntityState.Added)
            {
                if (e.Entry.Entity is ITimeTracked timeTrackedEntity) // Automatically set CreatedAt and UpdatedAt for CREATED entities 
                {
                    timeTrackedEntity.CreatedAt = DateTime.Now;
                    timeTrackedEntity.UpdatedAt = timeTrackedEntity.CreatedAt;
                }
            }
        }

        partial void OnEntityStateChangedPartial(object sender, EntityStateChangedEventArgs e)
        {
            if (e.NewState == EntityState.Modified)
            {
                if (e.Entry.Entity is ITimeTracked timeTrackedEntity) // Automatically update ModifiedDate for UPDATED entities 
                {
                    timeTrackedEntity.UpdatedAt = DateTime.Now;
                }
            }
        }

    }
}
