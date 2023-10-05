using Microsoft.EntityFrameworkCore;

namespace CryptoTraderDataHelperAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    { }
}
