
namespace TvMaze.Test.xUnit
{
    public static class BaseTestHelper
    {
        public static string ToUserServiceUrl(this string route)
        {
            if (route == null)
                return null;

            return string.Format("/api/tvmaze-service/v1/{0}", route.Trim().TrimStart('/'));
        }
    }
}
