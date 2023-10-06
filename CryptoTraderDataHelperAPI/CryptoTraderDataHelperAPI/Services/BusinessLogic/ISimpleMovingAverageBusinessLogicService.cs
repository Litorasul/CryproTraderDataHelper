using CryptoTraderDataHelperAPI.DTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic
{
    public interface ISimpleMovingAverageBusinessLogicService
    {
        SimpleMovingAverageDto CalculateSimpleMovingAverage(string symbol, int dataPoints, int timeAmount, TimePeriod timePeriod);
        SimpleMovingAverageDto CalculateSimpleMovingAverage(string symbol, int dataPoints, int timeAmount, TimePeriod timePeriod, DateTime startDate);
    }
}