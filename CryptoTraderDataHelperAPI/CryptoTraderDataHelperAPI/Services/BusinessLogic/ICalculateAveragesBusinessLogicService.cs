using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic
{
    public interface ICalculateAveragesBusinessLogicService
    {
        DailyAveragImportDto CalculateDailyAverageForSymbol(int symbolId);
        MinutelyAverageImportDto CalculateMinutelyAverageForSymbol(int symbolId);
        WeeklyAverageImportDto CalculateWeeklyAverageForSymbol(int symbolId);
    }
}