using LolStatsApp.Core.Models;
using LolStatsApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LolStatsApp.Infrastructure.Repositories;

public class PlayerStatsRepository(AppDbContext dbContext)
{
    public async Task<PlayerStats?> GetPlayerStatsAsync(string summonerName)
    {
        return await dbContext.PlayerStats
            .FirstOrDefaultAsync(p => p.SummonerName == summonerName);
    }

    public async Task AddOrUpdatePlayerStatsAsync(PlayerStats stats)
    {
        var existing = await dbContext.PlayerStats
            .FirstOrDefaultAsync(p => p.SummonerName == stats.SummonerName);

        if (existing == null)
            await dbContext.PlayerStats.AddAsync(stats);
        else
        {
            existing.Wins = stats.Wins;
            existing.Losses = stats.Losses;
        }

        await dbContext.SaveChangesAsync();
    }
}