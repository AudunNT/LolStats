using System.Net;
using System.Net.Http.Json;
using Bogus;
using Bogus.DataSets;
using LolStatsApp.Controllers;
using LolStatsApp.Core.Contracts;
using LolStatsApp.Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;

namespace LolStatsApp.Tests.Integration.StatsController;

public class StatsControllerTests(StatsApiFactory apiFactory) : IClassFixture<StatsApiFactory>
{
    private readonly StatsApiFactory _apiFactory = apiFactory;
    private readonly HttpClient _client = apiFactory.CreateClient();
    private const string ValidSummonerName = "Kjøtve";
    
    private readonly Faker<PlayerStats> _playerStatsFaker = new Faker<PlayerStats>()
        .RuleFor(x => x.Wins, f => f.Random.Int(1, 100))
        .RuleFor(x => x.Losses, f => f.Random.Int(1, 100))
        .RuleFor(x => x.SummonerName, ValidSummonerName);

    [Fact]
    public async Task Create_CreatesUser_WhenDataIsValid()
    {
        // Arrange
        var playerStats = _playerStatsFaker.Generate();
        
        // Act
        var response = await _client.GetAsync($"api/stats/{ValidSummonerName}", TestContext.Current.CancellationToken);
        // Assert
        var playerStatsResponse = await response.Content.ReadFromJsonAsync<PlayerStats>(TestContext.Current.CancellationToken);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(playerStats.SummonerName, playerStatsResponse!.SummonerName);
    }
}