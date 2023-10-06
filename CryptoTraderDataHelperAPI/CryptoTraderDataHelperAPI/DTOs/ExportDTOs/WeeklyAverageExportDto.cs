namespace CryptoTraderDataHelperAPI.DTOs.ExportDTOs;

public class WeeklyAverageExportDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public int WeekNumber { get; set; }
    public int Year { get; set; }
    public int SymbolId { get; set; }
    public SymbolsExportDto Symbol { get; set; }
}
