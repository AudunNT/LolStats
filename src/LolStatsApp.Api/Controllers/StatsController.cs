using Microsoft.AspNetCore.Mvc;
using LolStatsApp.Infrastructure.Repositories;
using LolStatsApp.Core.Models;

namespace LolStatsApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController(PlayerStatsRepository repo) : ControllerBase
{
    [HttpGet("{summonerName}")]
    public async Task<IActionResult> GetStats(string summonerName)
    {
        var stats = await repo.GetPlayerStatsAsync(summonerName);
        if (stats != null) return Ok(stats);
        
        // Create default if non-existent
        stats = new PlayerStats
        {
            SummonerName = summonerName,
            Wins = 0,
            Losses = 0
        };
        await repo.AddOrUpdatePlayerStatsAsync(stats);

        return Ok(stats);
    }
}