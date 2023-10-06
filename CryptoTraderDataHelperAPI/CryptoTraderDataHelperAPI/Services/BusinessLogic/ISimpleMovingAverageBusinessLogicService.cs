using CryptoTraderDataHelperAPI.DTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic
{
    public interface ISimpleMovingAverageBusinessLogicService
    {
        SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod);
        SimpleMovingAverageDto CalculateSimpleMovingAverage(int symbolId, int dataPoints, int timeAmount, TimePeriod timePeriod, DateTime startDate);
    }
}