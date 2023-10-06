using CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
using CryptoTraderDataHelperAPI.DTOs.ImportDTOs;
using CryptoTraderDataHelperAPI.Services.DataAccess;
using System.Globalization;

namespace CryptoTraderDataHelperAPI.Services.BusinessLogic;

public class CalculateAveragesBusinessLogicService : ICalculateAveragesBusinessLogicService
{
    private readonly IMinutelyAverageDataAccessService _minutelyAverageDataAccessService;
    private readonly IDailyAverageDataAccessService _dailyAverageDataAccessService;
    private readonly IWeeklyAverageDataAccessService _weeklyAverageDataAccessService;
    private readonly ITradeDataAccessService _tradeDataAccessService;

    public CalculateAveragesBusinessLogicService(IMinutelyAverageDataAccessService minutelyAverageDataAccessService, IDailyAverageDataAccessService dailyAverageDataAccessService, IWeeklyAverageDataAccessService weeklyAverageDataAccessService, ITradeDataAccessService tradeDataAccessService)
    {
        _minutelyAverageDataAccessService = minutelyAverageDataAccessService;
        _dailyAverageDataAccessService = dailyAverageDataAccessService;
        _weeklyAverageDataAccessService = weeklyAverageDataAccessService;
        _tradeDataAccessService = tradeDataAccessService;
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
            return null;
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
            return null;
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
            return null;
        }
        var average = new WeeklyAverageImportDto
        {
            SymbolId = symbolId,
            WeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(from, CalendarWeekRule.FirstDay, DayOfWeek.Monday),
            Year = from.Year,
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
