using CryptoTraderDataHelperAPI.DTOs;
using CryptoTraderDataHelperAPI.Services.DataAccess;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic;

public class SimpleMovingAverageBusinessLogicService : ISimpleMovingAverageBusinessLogicService
{
    private readonly IMinutelyAverageDataAccessService _minutelyDataAccessService;
    private readonly IDailyAverageDataAccessService _dailyAverageDataAccessService;
    private readonly IWeeklyAverageDataAccessService _weeklyAverageDataAccessService;
    public SimpleMovingAverageBusinessLogicService(IMinutelyAverageDataAccessService minutelyDataAccessService, IDailyAverageDataAccessService dailyAverageDataAccessService, IWeeklyAverageDataAccessService weeklyAverageDataAccessService)
    {
        _minutelyDataAccessService = minutelyDataAccessService;
        _dailyAverageDataAccessService = dailyAverageDataAccessService;
        _weeklyAverageDataAccessService = weeklyAverageDataAccessService;
    }

    public SimpleMovingAverageDto CalculateSimpleMovingAverage(string symbol, int dataPoints, int timeAmount, TimePeriod timePeriod)
    {
        return CalculateSimpleMovingAverage(symbol, dataPoints, timeAmount, timePeriod, DateTime.Now);
    }

    public SimpleMovingAverageDto CalculateSimpleMovingAverage(string symbol, int dataPoints, int timeAmount, TimePeriod timePeriod, DateTime startDate)
    {
        var result = new SimpleMovingAverageDto();
        result.Symbol = symbol;

        var source = GetSource(Common.SYMBOL_IDS[symbol], timeAmount, timePeriod, startDate);

        result.Average = SimpleMovingAverageCalculator(source, dataPoints);

        return result;
    }

    private List<double> GetSource(int symboiId, int timeAmount, TimePeriod timePeriod, DateTime startDate)
    {
        throw new NotImplementedException();
    }

    private List<double> SimpleMovingAverageCalculator(List<double> source, int length)
    {
        var sample = new Queue<double>(length);
        var result = new List<double>();
        foreach (var d in source)
        {
            if (sample.Count == length)
            {
                sample.Dequeue();
            }
            sample.Enqueue(d);
            result.Add(sample.Average());
        }
        return result;
    }
}
