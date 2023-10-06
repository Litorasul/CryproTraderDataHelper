using CryptoTraderDataHelperAPI.Services.BusinessLogic;
using CryptoTraderDataHelperAPI.Services.DataAccess;

namespace CryptoTraderDataHelperAPI.Services.BackgroundServices;

public class WeeklyAverageBackgroundService : BackgroundService
{
    //Running every week
    private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromDays(1));
    private readonly IServiceProvider _serviceProvider;

    public WeeklyAverageBackgroundService(IServiceProvider serviceProvider)
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
                var dbService = scope.ServiceProvider.GetRequiredService<IWeeklyAverageDataAccessService>();

                foreach (var keyValuePair in Common.SYMBOL_IDS)
                {
                    var average = calculateService.CalculateWeeklyAverageForSymbol(keyValuePair.Value);
                    if (average != null)
                    {
                        await dbService.AddNewWeeklyAverageAsync(average);
                    }
                }
            }
        }
    }
}
