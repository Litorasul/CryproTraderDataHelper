namespace ConsoleClient_CryptoTraderDataHelper;

internal class SimpleMovingAverage
{
    public string Symbol { get; set; }
    public List<double> Average { get; set; }

    public SimpleMovingAverage()
    {
        Average = new List<double>();
    }
}
