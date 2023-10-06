namespace CryptoTraderDataHelperAPI.DTOs.ExportDTOs;

public class TradeExportDto
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public double Price { get; set; }
    public int SymbolId { get; set; }
    public SymbolsExportDto Symbol { get; set; }
    public int? MinutelyAverageId { get; set; }
    public MinutelyAverageExportDto? MinutelyAverage { get; set; }
    public int? DailyAverageId { get; set; }
    public DailyAverageExportDto? DailyAverage { get; set; }
    public int? WeeklyAverageId { get; set; }
    public WeeklyAverageExportDto? WeeklyAverage { get; set; }
}
