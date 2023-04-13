using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Models.Schedule;
using TvMaze.Domain;

namespace TvMaze.Core.Services.Shows
{
    public partial class ShowService
    {
        public async Task SaveShowUrlsAsync(List<ScheduleOverview> scheduleOverviewList)
        {
            if (scheduleOverviewList == null)
            {
                return;
            }

            var showLinks = scheduleOverviewList.Select(s => s._links.show.href).Distinct().ToList();

            var oldLinks = await _context.ShowLinks.Where(l => !showLinks.Contains(l.Url)).ToListAsync();
            if (oldLinks.Any())
            {
                _context.ShowLinks.RemoveRange(oldLinks);
                await _context.SaveChangesAsync();
            }

            var existingLinks = await _context.ShowLinks.Select(l => l.Url).AsNoTracking().ToListAsync();
            showLinks.RemoveAll(link => existingLinks.Contains(link));

            if (showLinks.Any())
            {
                var newLinks = showLinks.Select(link => new ShowLink() { Url = link }).ToList();

                await _context.ShowLinks.AddRangeAsync(newLinks);
                await _context.SaveChangesAsync();
            }
        }
    }
}