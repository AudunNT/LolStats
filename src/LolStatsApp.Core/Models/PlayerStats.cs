using System.ComponentModel.DataAnnotations;

namespace LolStatsApp.Core.Models;

public class PlayerStats
{
    [Key]
    public string SummonerName { get; set; } = string.Empty;
    public int Wins { get; set; }
    public int Losses { get; set; }

    public double WinRate => Wins + Losses == 0 ? 0 : (double)Wins / (Wins + Losses);
}