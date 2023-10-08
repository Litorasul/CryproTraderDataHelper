using CryptoTraderDataHelperAPI.DTOs;
using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Services.DataAccess;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic;

public class CalculateAveragesBusinessLogicService : ICalculateAveragesBusinessLogicService
{
    private readonly ITradeDataAccessService _tradeDataAccessService;

    public CalculateAveragesBusinessLogicService(ITradeDataAccessService tradeDataAccessService)
    {
        _tradeDataAccessService = tradeDataAccessService;
    }

    public Last24HoursDto CalculateLast24HoursAverage(int symbolId)
    {
        var from  = DateTime.Now.Subtract(TimeSpan.FromDays(1));
        var to = DateTime.Now;
        var trades = new List<TradeExportDto>();
        try
        {
            trades = _tradeDataAccessService.GetAllTradesForATimePeriodForASymbol(from, to, symbolId);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        var average = new Last24HoursDto
        {
            Price = AveragePrice(trades),
            Symbol = trades[0].Symbol.Name
        };
        return average;
    }

    public MinutelyAverageImportDto CalculateMinutelyAverageForSymbol(int symbolId)
    {
        // Working 5 minutes behind cuttent time, still not reliable enough
        var from = DateTime.Now.Subtract(TimeSpan.FromMinutes(6));
        var to = DateTime.Now.Subtract(TimeSpan.FromMinutes(5));
        var trades = new List<TradeExportDto>();
        try
        {
            trades = _tradeDataAccessService.GetAllTradesForATimePeriodForASymbol(from, to, symbolId);
        }
        catch (NullReferenceException)
        {
            throw;
        }

        var average = new MinutelyAverageImportDto
        {
            SymbolId = symbolId,
            Time = to,
            Price = AveragePrice(trades),
        };
        return average;
    }

    public DailyAveragImportDto CalculateDailyAverageForSymbol(int symbolId)
    {
        // Calculating for Yesterday 
        var from = DateTime.Today.Subtract(TimeSpan.FromDays(1));
        var to = DateTime.Today;
        var trades = new List<TradeExportDto>();
        try
        {
            trades = _tradeDataAccessService.GetAllTradesForATimePeriodForASymbol(from, to, symbolId);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        var average = new DailyAveragImportDto
        {
            SymbolId = symbolId,
            Time = DateOnly.FromDateTime(from),
            Price = AveragePrice(trades),
        };
        return average;
    }

    public WeeklyAverageImportDto CalculateWeeklyAverageForSymbol(int symbolId)
    {
        //Calculating for last week
        var to = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
        var from = to.Subtract(TimeSpan.FromDays(7));
        var trades = new List<TradeExportDto>();
        try
        {
            trades = _tradeDataAccessService.GetAllTradesForATimePeriodForASymbol(from, to, symbolId);
        }
        catch (NullReferenceException)
        {
            throw;
        }
        var average = new WeeklyAverageImportDto
        {
            SymbolId = symbolId,
            Time = DateOnly.FromDateTime(from),
            Price = AveragePrice(trades)
        };
        return average;
    }

    private double AveragePrice(List<TradeExportDto> trades)
    {
        var sum = trades.Sum(t => t.Price);
        return sum / trades.Count;
    }
}
