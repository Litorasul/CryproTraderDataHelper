using ConsoleClient_CryptoTraderDataHelper;

var client = HttpClientFactory.Create();
var access = new DataAccess(client);

static void Greet()
{
    Console.WriteLine("To get the average for the last 24 hours please type: 24h {symbol}!");
    Console.WriteLine("To get the simple moving average please type: sma {symbol} {n} {p} {s} where 'n' is data points amount, 'p' is time period, 's' is starting date! ");
    Console.WriteLine("To exit the applocation type: end! ");
}

Console.WriteLine("Wellcome to Crypto Trader Data Helper!");
Greet();
string comand = Console.ReadLine();
while (comand.ToLower() != "end")
{
    var comandArray = comand.Split(' ');
    if (comandArray.Length == 2 && comandArray[0] == "24h")
    {
        var symbol = comandArray[1];
        var result = await access.GetLast24HoursAverageForASymbol(symbol);
        Console.WriteLine($"The Average for the last 24 hours for {symbol} is: {result.Price}!");
    }
    else if (comandArray.Length >= 4 && comandArray[0] == "sma")
    {
         var symbol = comandArray[1];
        var n = comandArray[2];
        var p = comandArray[3];
        var s = comandArray.Length == 5 ? comandArray[4] : DateTime.Now.ToString();
        var result = await access.GetSimpleMovingAverage(symbol, n, p, s);
        Console.WriteLine($"The requested Simple Moving Average for {symbol} is:!");
        foreach (var item in result.Average)
        {
            Console.WriteLine($"- {item},");
        }
    }
    Greet();
    comand = Console.ReadLine();
}
