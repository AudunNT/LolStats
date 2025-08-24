using LolStatsApp.Core.Models;

namespace LolStatsApp.Core.Contracts;

public interface IRiotApiClient
{
    Task<SummonerDto> GetSummonerByNameAsync(string summonerName);
    Task<IEnumerable<LeagueEntryDto>> GetRankedStatsAsync(string summonerId);
    Task<IEnumerable<string>> GetRecentMatchIdsAsync(string puuid, int count = 10);
    Task<MatchDto> GetMatchDetailsAsync(string matchId);
}