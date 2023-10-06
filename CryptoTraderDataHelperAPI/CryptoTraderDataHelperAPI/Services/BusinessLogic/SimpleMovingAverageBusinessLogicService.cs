using CryptoTraderDataHelperAPI.DTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic;

public class SimpleMovingAverageBusinessLogicService : ISimpleMovingAverageBusinessLogicService
{
    public SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod)
    {
        return CalculateSimpleMovingAverage(symbolId, dataPoints, timeAmount, timePeriod, DateTime.Now);
    }

    public SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod, DateTime startDate)
    {
        return new SimpleMovingAverageDto();
    }
}
