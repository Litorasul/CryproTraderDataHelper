using CryptoTraderDataHelperAPI.Services.BusinessLogic;
using CryptoTraderDataHelperAPI.Services.DataAccess;

namespace CryptoTraderDataHelperAPI.Services.BackgroundServices;

public class MinutelyAverageBackgroundService : BackgroundService
{
    //Running every minute
    private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
    private readonly IServiceProvider _serviceProvider;

    public MinutelyAverageBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var calculateService = scope.ServiceProvider.GetRequiredService<ICalculateAveragesBusinessLogicService>();
                var dbService = scope.ServiceProvider.GetRequiredService<IMinutelyAverageDataAccessService>();

                foreach (var keyValuePair in Common.SYMBOL_IDS)
                {
                    var average = calculateService.CalculateMinutelyAverageForSymbol(keyValuePair.Value);
                    if (average != null)
                    {
                        await dbService.AddNewMinutelyAverageAsync(average);
                    }
                }
            }
        }
    }
}
