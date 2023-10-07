using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface IWeeklyAverageDataAccessService
    {
        Task<int> AddNewWeeklyAverageAsync(WeeklyAverageImportDto importDto);
        List<WeeklyAverageExportDto> GetAllWeeklyAveragesForATimePeriod(DateOnly from, DateOnly to);
        List<WeeklyAverageExportDto> GetAllWeeklyAveragesForASymbolForATimePeriod(DateOnly from, DateOnly to, int symboId);
    }
}