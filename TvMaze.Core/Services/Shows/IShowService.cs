using TvMaze.Core.Models.Schedule;

namespace TvMaze.Core.Services.Shows
{
    public interface IShowService
    {
        Task SaveShowUrlsAsync(List<ScheduleOverview> scheduleOverviewList);
    }
}
