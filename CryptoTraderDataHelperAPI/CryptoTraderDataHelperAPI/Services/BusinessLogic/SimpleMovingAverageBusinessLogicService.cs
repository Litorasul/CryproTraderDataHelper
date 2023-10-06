using CryptoTraderDataHelperAPI.DTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic;

public class SimpleMovingAverageBusinessLogicService : ISimpleMovingAverageBusinessLogicService
{
    public SimpleMovingAverageBusinessLogicService()
    {
    }

    public SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod)
    {
        return CalculateSimpleMovingAverage(symbolId, dataPoints, timeAmount, timePeriod, DateTime.Now);
    }

    public SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod, DateTime startDate)
    {
        return new SimpleMovingAverageDto();
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
