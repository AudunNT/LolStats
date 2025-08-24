using LolStatsApp.Infrastructure.Data;
using LolStatsApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// PostgresSQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Repositories
builder.Services.AddScoped<MockStatsRepository>();
builder.Services.AddScoped<PlayerStatsRepository>();


var app = builder.Build();

app.MapControllers();

app.Run();