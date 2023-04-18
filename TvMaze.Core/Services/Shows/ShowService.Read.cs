using Microsoft.EntityFrameworkCore;
using TvMaze.Core.Extensions;
using TvMaze.Core.Models;
using TvMaze.Core.Models.ApiResponse;

namespace TvMaze.Core.Services.Shows
{
    public partial class ShowService
    {
        public async Task<List<string>> GetActualShowUrlsAsync()
        {
            return await _context.ShowLinks.Select(x => x.Url).ToListWithNoLockAsync();
        }

        public async Task<ServiceModelResult<List<ShowCastOverviewResponse>>> GetShowsWithCastAsync(int page, int size)
        {
            var result = new ServiceModelResult<List<ShowCastOverviewResponse>>();

            var take = Math.Max(1, size);
            var skip = (Math.Max(1, page) - 1) * take;

            var shows = await _context.Shows
                .Include(s => s.ShowCastRelation)
                .ThenInclude(r => r.CastPersone)
                .OrderBy(s => s.ShowId)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToListWithNoLockAsync();

            result.Model = shows.Select(s => new ShowCastOverviewResponse()
            {
                Id = s.ShowId,
                Name = s.Name,
                Cast = s.ShowCastRelation?.OrderByDescending(r => r.CastPersone.Birthday).Select(c => new ShowCastPersoneOverviewResponse()
                {
                    Id = c.CastPersone.PersonId,
                    Name = c.CastPersone.Name,
                    Birthday = c.CastPersone.Birthday
                }).ToList()
            }).ToList();

            return result;
        }
    }
}