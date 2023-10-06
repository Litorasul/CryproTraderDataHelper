using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface IDailyAverageDataAccessService
    {
        Task<int> AddNewDailyAverageAsync(DailyAveragImportDto importDto);
        List<DailyAverageExportDto> GetAllDailyAveragesForATimePeriod(DateOnly from, DateOnly to);
    }
}