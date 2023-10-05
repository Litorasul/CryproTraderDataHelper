using CryptoTraderDataHelperAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTraderDataHelperAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    { }

    public DbSet<Symbol> Symbols { get; set; }
    public DbSet<MinutelyAverage> MinutelyAverages { get; set; }
    public DbSet<DailyAverage> DailyAverages { get; set; }
    public DbSet<WeeklyAverage> WeeklyAverages { get; set; }
    public DbSet<Trade> Trades { get; set; }
}
