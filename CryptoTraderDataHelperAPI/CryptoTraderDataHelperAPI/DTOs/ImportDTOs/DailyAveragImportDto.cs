namespace CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

public class DailyAveragImportDto
{
    public double Price { get; set; }
    public DateOnly Time { get; set; }
    public int SymbolId { get; set; }
}
