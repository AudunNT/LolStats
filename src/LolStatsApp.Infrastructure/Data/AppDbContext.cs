using LolStatsApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LolStatsApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PlayerStats> PlayerStats { get; set; } = null!;
}