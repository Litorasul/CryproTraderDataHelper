namespace CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

public class WeeklyAverageImportDto
{
    public double Price { get; set; }
    public int WeekNumber { get; set; }
    public int Year { get; set; }
    public int SymbolId { get; set; }
}
