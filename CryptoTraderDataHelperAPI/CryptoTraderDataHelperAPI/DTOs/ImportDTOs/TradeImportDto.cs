namespace CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

public class TradeImportDto
{
    public DateTime Time { get; set; }
    public double Price { get; set; }
    public int SymbolId { get; set; }
    public int? MinutelyAverageId { get; set; }
    public int? DailyAverageId { get; set; }
    public int? WeeklyAverageId { get; set; }
}
