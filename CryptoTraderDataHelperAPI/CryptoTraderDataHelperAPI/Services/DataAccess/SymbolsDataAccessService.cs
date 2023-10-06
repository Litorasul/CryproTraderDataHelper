using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;

namespace CryptoTraderDataHelperAPI.Services.DataAccess;

public class SymbolsDataAccessService : ISymbolsDataAccessService
{
    private readonly AppDbContext _context;

    public SymbolsDataAccessService(AppDbContext context)
    {
        _context = context;
    }

    public List<SymbolsExportDto> GetAllSymbols()
    {
        var symbols = _context.Symbols.ToList();
        if (symbols == null)
        {
            throw new NullReferenceException("No Symbols found in the Database!");
        }
        var result = new List<SymbolsExportDto>();
        foreach (var symbol in symbols)
        {
            var dto = new SymbolsExportDto
            {
                Id = symbol.Id,
                Name = symbol.Name
            };
            result.Add(dto);
        }

        return result;
    }

    public SymbolsExportDto GetSymbolById(int id)
    {
        var symbol = _context.Symbols.FirstOrDefault(x => x.Id == id);
        if (symbol == null) { throw new NullReferenceException($"Symbol with ID: {id} does not exist in the Database!"); };

        return new SymbolsExportDto { Id = symbol.Id, Name = symbol.Name };
    }
}
