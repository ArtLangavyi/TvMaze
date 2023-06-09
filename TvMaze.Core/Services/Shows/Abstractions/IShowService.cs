﻿using TvMaze.Core.Clients.TvMaze.Models.Schedule;
using TvMaze.Core.Clients.TvMaze.Models.Show;
using TvMaze.Core.Models;
using TvMaze.Core.Models.ApiResponse;

namespace TvMaze.Core.Services.Shows
{
    public interface IShowService
    {
        Task SaveShowUrlsAsync(List<ScheduleOverviewResponse> scheduleOverviewList);
        Task<List<string>> GetActualShowUrlsAsync();
        Task SaveShowWithCastAsync(List<ShowDetailResponse> showDetailResponseList);
        Task<ServiceModelResult<List<ShowCastOverviewResponse>>> GetShowsWithCastAsync(int page, int size);
    }
}
