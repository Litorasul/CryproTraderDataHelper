using CryptoTraderDataHelperAPI.Models;

namespace CryptoTraderDataHelperAPI.DTOs.ExportDTOs;
public class DailyAverageExportDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public DateOnly Time { get; set; }
    public int SymbolId { get; set; }
    public SymbolsExportDto Symbol { get; set; }
}
