using Microsoft.AspNetCore.Mvc;
using TvMaze.Workers.Models.Settings;

namespace TvMaze.Helpers
{
    public static class ResponseCacheHelper
    {

        public const string Public5minVaryByAllQueryKeys = "Public5minVaryByAllQueryKeys";
        public static void RegisterCacheProfiles(IDictionary<string, CacheProfile> cacheProfiles, TvMazeApiSettings cacheConfigSettings)
        {
            cacheProfiles.Add(Public5minVaryByAllQueryKeys, new CacheProfile
            {
                Duration = cacheConfigSettings.PublicCacheEnabled ? (5 * 60) : 0,
                Location = cacheConfigSettings.PublicCacheEnabled ? ResponseCacheLocation.Any : ResponseCacheLocation.None,
                NoStore = !cacheConfigSettings.PublicCacheEnabled,
                VaryByQueryKeys = new[] { "*" },
                VaryByHeader = "Accept-Language"
            });
        }
    }
}
