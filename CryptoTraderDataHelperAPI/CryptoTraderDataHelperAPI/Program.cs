using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.Models;
using CryptoTraderDataHelperAPI.Services.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("DataSource=app.db"));

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Services
builder.Services.AddTransient<ISymbolsDataAccessService, SymbolsDataAccessService>();
builder.Services.AddTransient<IMinutelyAverageDataAccessService, MinutelyAverageDataAccessService>();
builder.Services.AddTransient<IDailyAverageDataAccessService, DailyAverageDataAccessService>();
builder.Services.AddTransient<IWeeklyAverageDataAccessService, WeeklyAverageDataAccessService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply Migrations & seed symbols
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();

    var symbolsToSeed = new[] { "btcusdt", "ethusdt", "adausdt" };
    foreach (var symbol in symbolsToSeed)
    {
        if (!dbContext.Symbols.Any(s => s.Name == symbol))
        {
            await dbContext.Symbols.AddAsync(new Symbol { Name = symbol});
        }
    }
    dbContext.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
