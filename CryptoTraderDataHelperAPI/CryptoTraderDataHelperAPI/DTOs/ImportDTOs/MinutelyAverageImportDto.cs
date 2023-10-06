namespace CryptoTraderDataHelperAPI.DTOs.ImportDTOs;

public class MinutelyAverageImportDto
{
    public double Price { get; set; }
    public DateTime Time { get; set; }
    public int SymbolId { get; set; }
}
