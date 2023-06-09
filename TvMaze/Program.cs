using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Net;
using TvMaze.Clients;
using TvMaze.Core.Services.Shows;
using TvMaze.Data.Context;
using TvMaze.Data.Interceptors;
using TvMaze.Helpers;
using TvMaze.Services;
using TvMaze.Workers;
using TvMaze.Workers.Models.Settings;

var _nonLocalEnv = new List<string> { "Test", "Acc", "Prod", "Scrum" };
bool IsDevelopment() => !_nonLocalEnv.Contains(System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true)
    .AddJsonFile($"appsettings.{System.Environment.MachineName}.json", true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();
configuration.GetSection("ConnectionSettings").Bind(new ConnectionSettings());
configuration.GetSection("TvMaze.Api.Settings").Bind(new TvMazeApiSettings());

var logger = LogService.AddLogger(configuration);
Log.Logger = logger.CreateLogger();


var builder = WebApplication.CreateBuilder(args);


var tvMazeApiSettings = configuration.GetSection("TvMaze.Api.Settings").Get<TvMazeApiSettings>();
builder.Services.AddSingleton(tvMazeApiSettings);


if (tvMazeApiSettings!.ProxyEnabled)
{
    builder.Services.AddHttpClient("tvmaze-api", o => { o.BaseAddress = new Uri(tvMazeApiSettings.BaseUrl); })
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
    {

        Proxy = new WebProxy(new Uri(tvMazeApiSettings.ProxyUri)),
        UseProxy = true
    }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
    {
        SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
        {
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
        },
        MaxConnectionsPerServer = int.MaxValue,
        EnableMultipleHttp2Connections = true
    });
}
else
{
    builder.Services.AddHttpClient("tvmaze-api", o =>
    {
        o.Timeout = TimeSpan.FromSeconds(tvMazeApiSettings.HttpClientTimeoutSeconds);
        o.BaseAddress = new Uri(tvMazeApiSettings.BaseUrl);
    }).ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
    {
        SslOptions = new System.Net.Security.SslClientAuthenticationOptions()
        {
            EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13,
        },
        MaxConnectionsPerServer = int.MaxValue,
        EnableMultipleHttp2Connections = true
    });
}


builder.Services.AddTransient<ITvMazeApiFactory, TvMazeApiFactory>();
builder.Services.AddTransient<IShowService, ShowService>();
builder.Services.AddTransient<IScraperScopeService, ScraperScopeService>();


builder.Services.AddDbContext<TvMazeContext>(o =>
{
    o.UseSqlServer(ConnectionSettings.DefaultConnection, b => b.MigrationsAssembly("TvMaze.Data"));

    if (IsDevelopment())
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

 builder.Services.AddHostedService<SchedulesIndexingService>();
 builder.Services.AddHostedService<ShowCastScaperService>();
 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TvMaze API", Version = "v1" });
});

builder.Services.AddResponseCaching();

builder.Services.AddControllers(o => ResponseCacheHelper.RegisterCacheProfiles(o.CacheProfiles, tvMazeApiSettings));

var app = builder.Build();

app.MapControllers();

app.UseSwagger();

app.MapGet("/", () => "Hello World!");

app.UseSwaggerUI();

app.UseResponseCaching();

await app.RunAsync();
