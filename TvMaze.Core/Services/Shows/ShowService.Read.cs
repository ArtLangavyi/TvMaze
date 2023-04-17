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

        public async Task<ServiceModelResult<List<ShowCastOverviewResponse>>> GetShowsWithCastAsync()
        {
            var result = new ServiceModelResult<List<ShowCastOverviewResponse>>();

            var shows = await _context.Shows
                .Include(s => s.ShowCastRelation)
                .ThenInclude(r => r.CastPersone)
                .OrderBy(s => s.ShowId)
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