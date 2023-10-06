using CryptoTraderDataHelperAPI.Services.BusinessLogic;
using CryptoTraderDataHelperAPI.Services.DataAccess;

namespace CryptoTraderDataHelperAPI.Services.BackgroundServices;

public class DailyAverageBackgroundService : BackgroundService
{
    //Running every day
    private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromDays(1));
    private readonly IServiceProvider _serviceProvider;

    public DailyAverageBackgroundService(IServiceProvider serviceProvider)
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
                var dbService = scope.ServiceProvider.GetRequiredService<IDailyAverageDataAccessService>();

                foreach (var keyValuePair in Common.SYMBOL_IDS)
                {
                    var average = calculateService.CalculateDailyAverageForSymbol(keyValuePair.Value);
                    if (average != null)
                    {
                        await dbService.AddNewDailyAverageAsync(average);
                    }
                }
            }
        }
    }
}
