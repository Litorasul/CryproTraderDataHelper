using Binance.Client.Websocket;
using Binance.Client.Websocket.Client;
using Binance.Client.Websocket.Subscriptions;
using Binance.Client.Websocket.Websockets;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Services.DataAccess;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace CryptoTraderDataHelperAPI.Services.BackgroundServices;

public class BinanceWebsocketBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Uri _url;

    public BinanceWebsocketBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _url = BinanceValues.ApiWebsocketUrl;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var tradeDataAccessService = scope.ServiceProvider.GetRequiredService<ITradeDataAccessService>();
            var symbolDataAccessService = scope.ServiceProvider.GetRequiredService<ISymbolsDataAccessService>();
            var exitEvent = new ManualResetEvent(false);
            var symbolDictionary = CreateNewSymbolDictionary(symbolDataAccessService);

                using (var communicator = new BinanceWebsocketCommunicator(_url))
            {
                using (var client = new BinanceWebsocketClient(communicator))
                {
                    client
                    .Streams
                    .TradesStream
                    //.ObserveOn(TaskPoolScheduler.Default)
                    .Subscribe(async response =>
                    {
                        var incomingTrade = response.Data;
                        if (incomingTrade != null)
                        {
                            //Save trade  in DB
                            var trade = new TradeImportDto
                            {
                                Price = incomingTrade.Price,
                                Time = incomingTrade.TradeTime,
                                SymbolId = symbolDictionary[incomingTrade.Symbol.ToLower()].Id
                            };
                            await tradeDataAccessService.AddNewTradeAsync(trade);
                        }
                    });

                    var symbols = symbolDataAccessService.GetAllSymbols();
                    var subscriptions = new SubscriptionBase[symbols.Count];
                    for (int i = 0; i < symbols.Count; i++)
                    {
                        var tradeSubscription = new TradeSubscription(symbols[i].Name);
                        subscriptions[i] = tradeSubscription;
                    }
                    client.SetSubscriptions(subscriptions);
                    await communicator.Start();
                    if (stoppingToken.IsCancellationRequested)
                    {
                        exitEvent.Close();
                    }
                }
            }
        }
    }

    private Dictionary<string, SymbolsExportDto> CreateNewSymbolDictionary(ISymbolsDataAccessService symbolDataAccessService)
    {
        var result = new Dictionary<string, SymbolsExportDto>();
        var symbols = symbolDataAccessService.GetAllSymbols();
        foreach ( var symbol in symbols)
        {
            result[symbol.Name] = symbol;
        };
        return result;
    }
}
