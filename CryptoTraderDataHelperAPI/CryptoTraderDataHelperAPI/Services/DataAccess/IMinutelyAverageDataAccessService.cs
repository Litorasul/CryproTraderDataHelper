using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface IMinutelyAverageDataAccessService
    {
        Task<int> AddNewMinutelyAverageAsync(MinutelyAverageImportDto importDto);
        List<MinutelyAverageExportDto> GetAllMinutelyAveragesForATimePeriod(DateTime from, DateTime to);
        List<MinutelyAverageExportDto> GetAllMinutelyAveragesForASymbolForATimePeriod(DateTime from, DateTime to, int symboId);
    }
}