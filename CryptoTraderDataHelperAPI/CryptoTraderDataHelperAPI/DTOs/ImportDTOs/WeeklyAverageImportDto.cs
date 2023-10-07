namespace CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

public class WeeklyAverageImportDto
{
    public double Price { get; set; }
    public DateOnly Time {  get; set; }
    public int SymbolId { get; set; }
}
