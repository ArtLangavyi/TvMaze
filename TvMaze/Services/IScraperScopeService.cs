namespace TvMaze.Services
{
    public interface IScraperScopeService
    {
        Task PullDataAsync();
        Task PullAllShowsFromSchedulesAsync();
    }
}
