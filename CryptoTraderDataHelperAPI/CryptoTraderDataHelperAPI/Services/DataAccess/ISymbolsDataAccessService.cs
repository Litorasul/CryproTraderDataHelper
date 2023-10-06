using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess
{
    public interface ISymbolsDataAccessService
    {
        List<SymbolsExportDto> GetAllSymbols();
        SymbolsExportDto GetSymbolById(int id);
    }
}