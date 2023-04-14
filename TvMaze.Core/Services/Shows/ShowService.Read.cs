using TvMaze.Core.Extensions;

namespace TvMaze.Core.Services.Shows
{
    public partial class ShowService
    {
        public async Task<List<string>> GetActualShowUrlsAsync()
        {
            return await _context.ShowLinks.Select(x => x.Url).ToListWithNoLockAsync();
        }
    }
}