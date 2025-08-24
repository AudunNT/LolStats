using System.Data;
using System.Net.Mime;
using LolStatsApp.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;

namespace LolStatsApp.Tests.Integration;

public class StatsApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private const string ValidSummonerName = "Kjøtve";
        
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithDatabase("lolStats")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
    
    private readonly RiotApiServer _riotApiServer = new();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(logging => { logging.ClearProviders(); });
        
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IHostedService));
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(AppDbContext));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
            
            services.AddHttpClient("Riot", httpClient =>
            {
                httpClient.BaseAddress = new Uri(_riotApiServer.Url);
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.Accept, "application/json");
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.UserAgent, $"Course-{Environment.MachineName}");
            });
        });
    }

    public async ValueTask InitializeAsync()
    {
        _riotApiServer.Start();
        _riotApiServer.SetUpUser(ValidSummonerName);
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }

    public new async ValueTask DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
        _riotApiServer.Dispose();
    }
}