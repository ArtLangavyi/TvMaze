using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;
using TvMaze.Data.Context;
using TvMaze.Data.Interceptors;
using TvMaze.Services;
using TvMaze.Workers;
using TvMaze.Workers.Clients;
using TvMaze.Workers.Models.Settings;

var _nonLocalEnv = new List<string> { "Test", "Acc", "Prod", "Scrum" };
bool IsDevelopment() => !_nonLocalEnv.Contains(System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TvMazeContext>(o =>
{
    o.UseSqlServer(ConnectionSettings.DefaultConnection, b => b.MigrationsAssembly("TvMaze.Data"));

    if (!_nonLocalEnv.Contains(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
    {
        o.EnableSensitiveDataLogging(true);
        o.AddInterceptors(new TraceCommandInterceptor());
    }
    else
    {
        o.ConfigureWarnings(w => w.Ignore(
            CoreEventId.FirstWithoutOrderByAndFilterWarning,
            RelationalEventId.MultipleCollectionIncludeWarning,
            CoreEventId.RowLimitingOperationWithoutOrderByWarning));
    }
});

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddJsonFile($"appsettings.{System.Environment.MachineName}.json", true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
configuration.GetSection("ConnectionSettings").Bind(new ConnectionSettings());
configuration.GetSection("TvMaze.Api.Settings").Bind(new TvMazeApiSettings());

var logger = LogService.AddLogger(configuration);
Log.Logger = logger.CreateLogger();

// Add services to the container.

var app = builder.Build();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseKestrel(options => options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5)))
    .ConfigureLogging((context, builder) => builder.AddConsole())
    .ConfigureServices(services =>
    {
        var tvMazeApiSettings = configuration.GetSection("TvMaze.Api.Settings").Get<TvMazeApiSettings>();

        services.AddSingleton(tvMazeApiSettings);
        services.AddTransient<IApiFactory, ApiFactory>();

        //services.AddHttpClient("tvmaze-api", o =>
        // {
        //     o.Timeout = new TimeSpan(0, 0, configuration.GetValue<int>("AppSettings:HttpClientTimeoutSeconds"));
        //     o.BaseAddress = new Uri(tvMazeApiSettings.BaseUrl);
        //     o.DefaultRequestVersion = new Version(2, 0);
        //     o.DefaultRequestHeaders.ConnectionClose = true;
        // });



        services.AddTransient<IScraperScopeService, ScraperScopeService>();
        //services.AddHostedService<ShowCastScaperWorker>();
        services.AddHostedService<SchedulesIndexingService>();
        
    })
    .Build();

await host.RunAsync();
