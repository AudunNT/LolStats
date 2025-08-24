namespace LolStatsApp.Core.Models
{
    public class SummonerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Puuid { get; set; }
        public int ProfileIconId { get; set; }
        public int SummonerLevel { get; set; }
    }

    public class LeagueEntryDto
    {
        public string Tier { get; set; }
        public string Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string QueueType { get; set; }
    }

    public class MatchDto
    {
        public string MatchId { get; set; }
        public int DurationSeconds { get; set; }
        // Add more fields as needed for champion stats, KDA, etc.
    }
}
