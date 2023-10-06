using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Models;

namespace CryptoTraderDataHelperAPI.Services.DataAccess;

public class DailyAverageDataAccessService : IDailyAverageDataAccessService
{
    private readonly AppDbContext _context;
    private readonly ISymbolsDataAccessService _symbolsDataAccessService;

    public DailyAverageDataAccessService(ISymbolsDataAccessService symbolsDataAccessService, AppDbContext context)
    {
        _symbolsDataAccessService = symbolsDataAccessService;
        _context = context;
    }

    public List<DailyAverageExportDto> GetAllDailyAveragesForATimePeriod(DateOnly from, DateOnly to)
    {
        var averages = _context.DailyAverages.Where(a => a.Time >= from && a.Time <= to).ToList();
        if (averages == null) { throw new NullReferenceException("No Daily averages for that period in the Database!"); };

        var result = new List<DailyAverageExportDto>();
        foreach (var average in averages)
        {
            var dto = new DailyAverageExportDto
            {
                Id = average.Id,
                Time = average.Time,
                Price = average.Price,
                SymbolId = average.SymbolId
            };
            dto.Symbol = _symbolsDataAccessService.GetSymbolById(average.SymbolId);
            result.Add(dto);
        }
        return result;
    }

    public async Task<int> AddNewDailyAverageAsync(DailyAveragImportDto importDto)
    {
        var average = new DailyAverage
        {
            Time = importDto.Time,
            Price = importDto.Price,
            SymbolId = importDto.SymbolId
        };

        await _context.DailyAverages.AddAsync(average);
        await _context.SaveChangesAsync();

        return average.Id;
    }
}
