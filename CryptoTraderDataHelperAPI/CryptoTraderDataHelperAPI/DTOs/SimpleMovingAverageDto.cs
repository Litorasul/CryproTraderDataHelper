namespace CryptoTraderDataHelperAPI.DTOs;

public class SimpleMovingAverageDto
{
    public string Symbol { get; set; }
    public List<double> Average { get; set; }
}
