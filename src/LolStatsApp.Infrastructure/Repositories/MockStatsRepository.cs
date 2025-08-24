using LolStatsApp.Core.Models;

namespace LolStatsApp.Infrastructure.Repositories;

public class MockStatsRepository
{
    public PlayerStats GetPlayerStats(string summonerName)
    {
        // Temporary hardcoded data
        return new PlayerStats
        {
            SummonerName = summonerName,
            Wins = 10,
            Losses = 5
        };
    }
}