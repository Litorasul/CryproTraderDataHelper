using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Models;

namespace CryptoTraderDataHelperAPI.Services.DataAccess;

public class MinutelyAverageDataAccessService : IMinutelyAverageDataAccessService
{
    private readonly AppDbContext _context;
    private readonly ISymbolsDataAccessService _symbolsDataAccessService;

    public MinutelyAverageDataAccessService(AppDbContext context, ISymbolsDataAccessService symbolsDataAccessService)
    {
        _context = context;
        _symbolsDataAccessService = symbolsDataAccessService;
    }

    public List<MinutelyAverageExportDto> GetAllMinutelyAveragesForATimePeriod(DateTime from, DateTime to)
    {
        var averages = _context.MinutelyAverages.Where(a => a.Time >= from && a.Time <= to).ToList();
        if (averages == null) { throw new NullReferenceException("No Minutely averages for that period in the Database!"); };

        var result = new List<MinutelyAverageExportDto>();
        foreach (var average in averages)
        {
            var dto = new MinutelyAverageExportDto
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

    public async Task<int> AddNewMinutelyAverageAsync(MinutelyAverageImportDto importDto)
    {
        var average = new MinutelyAverage
        {
            Time = importDto.Time,
            Price = importDto.Price,
            SymbolId = importDto.SymbolId
        };

        await _context.MinutelyAverages.AddAsync(average);
        await _context.SaveChangesAsync();

        return average.Id;
    }
}
