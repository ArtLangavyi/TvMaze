namespace TvMaze.Workers.Models.Settings
{
    public class TvMazeApiSettings
    {
        public static string Name { get; set; }

        public static string Maintainer { get; set; }

        public static string Environment { get; set; }
        public string BaseUrl { get; set; }
        public bool ProxyEnabled { get; set; }
        public string ProxyUri { get; set; }
        public int HttpClientTimeoutSeconds { get; set; }
        public bool PublicCacheEnabled { get; set; }
    }
}
