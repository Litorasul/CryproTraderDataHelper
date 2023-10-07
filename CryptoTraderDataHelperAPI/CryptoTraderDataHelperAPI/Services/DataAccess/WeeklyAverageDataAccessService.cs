using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Models;

namespace CryptoTraderDataHelperAPI.Services.DataAccess;

public class WeeklyAverageDataAccessService : IWeeklyAverageDataAccessService
{
    private readonly AppDbContext _context;
    private readonly ISymbolsDataAccessService _symbolsDataAccessService;
    public WeeklyAverageDataAccessService(AppDbContext context, ISymbolsDataAccessService symbolsDataAccessService)
    {
        _context = context;
        _symbolsDataAccessService = symbolsDataAccessService;
    }

    public List<WeeklyAverageExportDto> GetAllWeeklyAveragesForATimePeriod(DateOnly from, DateOnly to)
    {
        var averages = _context.WeeklyAverages.Where(a => a.Time >= from && a.Time <= to).ToList();
        if (averages == null) { throw new NullReferenceException("No Weekly averages for that period in the Database!"); };

        var result = new List<WeeklyAverageExportDto>();
        foreach (var average in averages)
        {
            var dto = new WeeklyAverageExportDto
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
    public List<WeeklyAverageExportDto> GetAllWeeklyAveragesForASymbolForATimePeriod(DateOnly from, DateOnly to, int symboId)
    {
        var averages = _context.WeeklyAverages.Where(a => a.Time >= from && a.Time <= to && a.SymbolId == symboId).ToList();
        if (averages == null) { throw new NullReferenceException("No Weekly averages for that period in the Database!"); };

        var result = new List<WeeklyAverageExportDto>();
        foreach (var average in averages)
        {
            var dto = new WeeklyAverageExportDto
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

    public async Task<int> AddNewWeeklyAverageAsync(WeeklyAverageImportDto importDto)
    {
        var average = new WeeklyAverage
        {
            Time = importDto.Time,
            Price = importDto.Price,
            SymbolId = importDto.SymbolId
        };

        await _context.WeeklyAverages.AddAsync(average);
        await _context.SaveChangesAsync();

        return average.Id;
    }
}
