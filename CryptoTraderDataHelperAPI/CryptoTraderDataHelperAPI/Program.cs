using Binance.Client.Websocket;
using Binance.Client.Websocket.Client;
using Binance.Client.Websocket.Subscriptions;
using Binance.Client.Websocket.Websockets;
using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.Models;
using CryptoTraderDataHelperAPI.Services.BackgroundServices;
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

//Data Access Services
builder.Services.AddTransient<ISymbolsDataAccessService, SymbolsDataAccessService>();
builder.Services.AddTransient<ITradeDataAccessService, TradeDataAccessService>();
builder.Services.AddTransient<IMinutelyAverageDataAccessService, MinutelyAverageDataAccessService>();
builder.Services.AddTransient<IDailyAverageDataAccessService, DailyAverageDataAccessService>();
builder.Services.AddTransient<IWeeklyAverageDataAccessService, WeeklyAverageDataAccessService>();

//Background Services
builder.Services.AddHostedService<BinanceWebsocketBackgroundService>();

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

// BinanceWebsocket
//using (IServiceScope scope = app.Services.CreateScope())
//{
//    var tradeDataAccessService = scope.ServiceProvider.GetRequiredService<ITradeDataAccessService>();
//    var symbolDataAccessService = scope.ServiceProvider.GetRequiredService<ISymbolsDataAccessService>();
//    var exitEvent = new ManualResetEvent(false);

//    using (var communicator = new BinanceWebsocketCommunicator(BinanceValues.ApiWebsocketUrl))
//    {
//        using (var client = new BinanceWebsocketClient(communicator))
//        {
//            client.Streams.TradesStream.Subscribe(response =>
//            {
//                var incomingTrade = response.Data;
//                if (incomingTrade != null)
//                {
//                    //Save in DB...incomingTrade.Symbol, incomingTrade.Price...
//                    Console.WriteLine($"Trade executed [{incomingTrade.Symbol}] price: {incomingTrade.Price}");
//                }
//            });

//            var symbols = symbolDataAccessService.GetAllSymbols();
//            var subscriptions = new SubscriptionBase[symbols.Count];
//            for (int i = 0; i < symbols.Count; i++)
//            {
//                var tradeSubscription = new TradeSubscription(symbols[i].Name);
//                subscriptions[i] = tradeSubscription;
//            }
//            client.SetSubscriptions(subscriptions);
//            await communicator.Start();
//        }
//    }
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
