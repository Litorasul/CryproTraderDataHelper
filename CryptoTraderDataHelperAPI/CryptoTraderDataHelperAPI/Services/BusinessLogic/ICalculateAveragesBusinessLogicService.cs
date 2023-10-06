using CryptoTraderDataHelperAPI.DTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic
{
    public interface ICalculateAveragesBusinessLogicService
    {
        Last24HoursDto CalculateLast24HoursAverage(int symbolId);
        DailyAveragImportDto CalculateDailyAverageForSymbol(int symbolId);
        MinutelyAverageImportDto CalculateMinutelyAverageForSymbol(int symbolId);
        WeeklyAverageImportDto CalculateWeeklyAverageForSymbol(int symbolId);
    }
}