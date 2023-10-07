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
    /// <summary>
    /// Calculate Simple Moving Average with starting DateTime now.
    /// </summary>
    /// <returns>View Data Transfer Object</returns>
    public SimpleMovingAverageDto CalculateSimpleMovingAverage(string symbol, int dataPoints, int timeAmount, TimePeriod timePeriod)
    {
        return CalculateSimpleMovingAverage(symbol, dataPoints, timeAmount, timePeriod, DateTime.Now);
    }
    /// <summary>
    /// Calculate Simple Moving Average with given start DateTime
    /// </summary>
    /// <returns>View Data Transfer Object</returns>
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
        var result = new List<double>();
        switch (timePeriod)
        {
            case TimePeriod.Minute:
                result = GetMinuteSource(symboiId, timeAmount, startDate);
                break;
            case TimePeriod.Day:
                result = GerDailySource(symboiId, timeAmount, startDate);
                break;
            case TimePeriod.Week:
                result = GetWeeklySource(symboiId, timeAmount, startDate);
                break;
            default:
                break;
        } 
        return result;
    }

    private List<double> GetWeeklySource(int symboiId, int timeAmount, DateTime startDate)
    {
        var from = DateOnly.FromDateTime(startDate.Subtract(TimeSpan.FromDays(timeAmount * 7)));
        var to = DateOnly.FromDateTime(startDate);
        var average = _weeklyAverageDataAccessService.GetAllWeeklyAveragesForASymbolForATimePeriod(from, to, symboiId);
        return average.Select(r => r.Price).ToList(); 
    }

    private List<double> GerDailySource(int symboiId, int timeAmount, DateTime startDate)
    {
        var from = DateOnly.FromDateTime(startDate.Subtract(TimeSpan.FromDays(timeAmount)));
        var to = DateOnly.FromDateTime(startDate);
        var average = _dailyAverageDataAccessService.GetAllDailyAveragesForASymbolForATimePeriod(from, to, symboiId);
        return average.Select(r => r.Price).ToList();
    }

    private List<double> GetMinuteSource(int symboiId, int timeAmount, DateTime startDate)
    {
        var from = startDate.Subtract(TimeSpan.FromMinutes(timeAmount));
        var average = _minutelyDataAccessService.GetAllMinutelyAveragesForASymbolForATimePeriod(from, startDate, symboiId);
        return average.Select(r => r.Price).ToList();
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
