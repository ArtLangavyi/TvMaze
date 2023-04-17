using TvMaze.Core.Clients.TvMaze.Models.Schedule;
using TvMaze.Core.Clients.TvMaze.Models.Show;

namespace TvMaze.Core.Services.Shows
{
    public interface IShowService
    {
        Task SaveShowUrlsAsync(List<ScheduleOverviewResponse> scheduleOverviewList);
        Task<List<string>> GetActualShowUrlsAsync();
        Task SaveShowWithCastAsync(List<ShowDetailResponse> showDetailResponseList);
    }
}
