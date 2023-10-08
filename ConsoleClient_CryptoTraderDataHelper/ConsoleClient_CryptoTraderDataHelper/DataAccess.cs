using Newtonsoft.Json;

namespace ConsoleClient_CryptoTraderDataHelper;

internal class DataAccess
{
    private readonly HttpClient _httpClient;

    public DataAccess(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    internal async Task<Last24> GetLast24HoursAverageForASymbol(string symbol)
    {
        var path = $"https://localhost:7110/api/{symbol}/24hAvgPrice";
        var result = new Last24();
        var response = await _httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Last24>(json);
        }

        return result;
    }

    internal async Task<SimpleMovingAverage> GetSimpleMovingAverage(string symbol, string n, string p, string s = null)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
           s = DateTime.Now.ToString();
        }
        var path = $"https://localhost:7110/api/{symbol}/SimpleMovingAverage?n={n}&p={p}&s={s}";
        var result = new SimpleMovingAverage();
        var response = await _httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<SimpleMovingAverage>(json);
        }

        return result;
    }
}
