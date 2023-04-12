using Elastic.Apm.SerilogEnricher;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;
using TvMaze.Workers.Models.Settings;

namespace TvMaze.Services
{
    public static class LogService
    {
        private static readonly List<string> NonLocalEnv = new List<string> { "Prod", "Acc", "Test", "Scrum" };

        public static LoggerConfiguration AddLogger(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.MachineName)
                .Enrich.WithProperty("Server", Environment.MachineName)
                .Enrich.WithProperty("Application", TvMazeApiSettings.Name);


            if (configuration.GetSection("ElasticApm").GetValue<bool>("Enabled"))
            {
                logger.Enrich.WithElasticApmCorrelationInfo();
            }

            if (NonLocalEnv.Contains(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
            {
                logger.WriteTo.Console(new RenderedCompactJsonFormatter());
            }
            else
            {
                logger.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code);
            }

            return logger;
        }
    }
}
