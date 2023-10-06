using CryptoTraderDataHelperAPI.Data;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Models;

namespace CryptoTraderDataHelperAPI.Services.DataAccess;

public class TradeDataAccessService : ITradeDataAccessService
{
    private readonly AppDbContext _context;
    private readonly ISymbolsDataAccessService _symbolsDataAccessService;
    public TradeDataAccessService(AppDbContext context, ISymbolsDataAccessService symbolsDataAccessService)
    {
        _context = context;
        _symbolsDataAccessService = symbolsDataAccessService;
    }

    public List<TradeExportDto> GetAllTradesForATimePeriodForASymbol(DateTime from, DateTime to, int symbolId)
    {
        var trades = _context.Trades.Where(t => t.Time >= from && t.Time <= to && t.SymbolId == symbolId).ToList();
        if (trades == null) { throw new NullReferenceException("No Trades for that period in the Database!"); };

        var result = new List<TradeExportDto>();
        foreach (var trade in trades)
        {
            var dto = new TradeExportDto
            {
                Id = trade.Id,
                Price = trade.Price,
                Time = trade.Time,
                SymbolId = trade.SymbolId,
                MinutelyAverageId = trade.MinutelyAverageId,
                DailyAverageId = trade.DailyAverageId,
                WeeklyAverageId = trade.WeeklyAverageId
            };
            dto.Symbol = _symbolsDataAccessService.GetSymbolById(trade.SymbolId);
            result.Add(dto);
        }
        return result;
    }

    public async Task<int> AddNewTradeAsync(TradeImportDto importDto)
    {
        var trade = new Trade
        {
            Price = importDto.Price,
            Time = importDto.Time,
            SymbolId = importDto.SymbolId,
            MinutelyAverageId = importDto.MinutelyAverageId,
            DailyAverageId = importDto.DailyAverageId,
            WeeklyAverageId = importDto.WeeklyAverageId
        };

        await _context.Trades.AddAsync(trade);
        await _context.SaveChangesAsync();

        return trade.Id;
    }

    public async Task UpdateMinutelyAverageForATradeAsync(int tradeId, int averageId)
    {
        var trade = _context.Trades.FirstOrDefault(t => t.Id == tradeId);
        if (trade == null) { throw new NullReferenceException($"Trade with ID: {tradeId} does not exist in the Database!"); }

        var average = _context.MinutelyAverages.FirstOrDefault(a => a.Id == averageId);
        if (average == null) { throw new NullReferenceException($"Minutely Average with ID: {averageId} does not exist in the Database!"); }

        trade.MinutelyAverageId = averageId;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDailyAverageForATradeAsync(int tradeId, int averageId)
    {
        var trade = _context.Trades.FirstOrDefault(t => t.Id == tradeId);
        if (trade == null) { throw new NullReferenceException($"Trade with ID: {tradeId} does not exist in the Database!"); }

        var average = _context.DailyAverages.FirstOrDefault(a => a.Id == averageId);
        if (average == null) { throw new NullReferenceException($"Daily Average with ID: {averageId} does not exist in the Database!"); }

        trade.DailyAverageId = averageId;
        await _context.SaveChangesAsync();
    }


    public async Task UpdateWeeklyAverageForATradeAsync(int tradeId, int averageId)
    {
        var trade = _context.Trades.FirstOrDefault(t => t.Id == tradeId);
        if (trade == null) { throw new NullReferenceException($"Trade with ID: {tradeId} does not exist in the Database!"); }

        var average = _context.WeeklyAverages.FirstOrDefault(a => a.Id == averageId);
        if (average == null) { throw new NullReferenceException($"Weekly Average with ID: {averageId} does not exist in the Database!"); }

        trade.WeeklyAverageId = averageId;
        await _context.SaveChangesAsync();
    }
}
