namespace TvMaze.Services
{
    public interface IScraperScopeService
    {
        Task<bool> PullDataAsync();
        Task PullAllShowsFromSchedulesAsync();
    }
}
