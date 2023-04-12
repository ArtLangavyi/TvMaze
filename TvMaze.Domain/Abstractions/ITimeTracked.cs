namespace TvMaze.Domain.Abstractions
{
    public interface ITimeTracked
    {
        DateTime CreatedAt { get; set; } // Auto-managed by EF, see IdentityContext.EventHandlers.cs
        DateTime UpdatedAt { get; set; } // Auto-managed by EF, see IdentityContext.EventHandlers.cs
    }
}
