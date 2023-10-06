namespace CryptoTraderDataHelperAPI.DTOs.ExportDTOs;

public class MinutelyAverageExportDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public DateTime Time { get; set; }
    public int SymbolId { get; set; }
    public SymbolsExportDto? Symbol {  get; set; }
}
