using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface IWeeklyAverageDataAccessService
    {
        Task<int> AddNewWeeklyAverageAsync(WeeklyAverageImportDto importDto);
        List<WeeklyAverageExportDto> GetAllWeeklyAveragesForATimePeriod(int from, int to, int year);
    }
}