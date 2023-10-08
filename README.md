# CryproTraderDataHelper

## Prerequisites
Before running this application, ensure you have the following prerequisites installed:

.NET SDK 8.0
SQLite (for database storage)


## Installation
1. Clone the repository to your local machine
2. Navigate to the project directory
3. Build and Run - CryptoTraderDataHelperAPI
4. Run ConsoleClient_CryptoTraderDataHelper or use Swagger or Postman to call the API

## Endpoints
### 1. Get 24-Hour Average Price
** Endpoint: GET /api/{symbol}/24hAvgPrice
Description: Returns the average price for the last 24 hours of data in the database or the oldest available price if 24 hours of data is not available.
Parameters:

- {symbol} - The symbol for which the average price is being calculated.
Example Request:
> GET /api/BTCUSDT/24hAvgPrice

Response (JSON):

```

{
  "price": 27751.9971875,
  "symbol": "btcusdt"
}

```
Response (XML):

```

<Last24HoursDto xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Price>27494.613636363636</Price>
    <Symbol>btcusdt</Symbol>
</Last24HoursDto>

```

### 2. Calculate Simple Moving Average (SMA)
** Endpoint: GET /api/{symbol}/SimpleMovingAverage
Description: Returns the current Simple Moving Average (SMA) of the symbol's price over a specified time period.
Parameters:

{symbol} - The symbol for which the SMA is being calculated.
n - The number of data points.
p - The time period represented by each data point (acceptable values: 1w, 1d, 30m, 5m, 1m).
s - (Optional) The datetime from which to start the SMA calculation (a date).
Example Requests:
> GET /api/BTCUSDT/SimpleMovingAverage?n=10&p=1d&s=2021-12-15
> GET /api/ADAUSDT/SimpleMovingAverage?n=100&p=1w
> GET /api/ETHUSDT/SimpleMovingAverage?n=200&p=5m

Response (JSON):

```
{
  "symbol": "btcusdt",
  "average": [
    27248.25,
    27248.25,
    27254.916666666668,
    26928.25,
    26601.583333333332,
    26271.583333333332,
    26274.916666666668
  ]
}

```

Responce (XML): 

```

<SimpleMovingAverageDto xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Symbol>btcusdt</Symbol>
    <Average>
        <double>27248.25</double>
        <double>27248.25</double>
        <double>27254.916666666668</double>
        <double>26928.25</double>
        <double>26601.583333333332</double>
        <double>26271.583333333332</double>
        <double>26274.916666666668</double>
    </Average>
</SimpleMovingAverageDto>

```

## Content Negotiation
The application supports content negotiation based on the Accept header. If the Accept header is set to application/json, the response will be in JSON format. If it is set to application/xml, the response will be in XML format.
