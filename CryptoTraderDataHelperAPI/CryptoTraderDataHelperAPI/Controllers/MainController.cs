using CryptoTraderDataHelperAPI.DTOs;
using CryptoTraderDataHelperAPI.Services.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTraderDataHelperAPI.Controllers;
[ApiController]
[Route("api/")]
public class MainController : ControllerBase
{
    private readonly ICalculateAveragesBusinessLogicService _calculatorService;
    private readonly ISimpleMovingAverageBusinessLogicService _simpleMovingAverageService;
    public MainController(ICalculateAveragesBusinessLogicService calculatorService, ISimpleMovingAverageBusinessLogicService simpleMovingAverageService)
    {
        _calculatorService = calculatorService;
        _simpleMovingAverageService = simpleMovingAverageService;
    }

    //GET .../api/{symbol}/24hAvgPrice
    [HttpGet("{symbol}/24hAvgPrice")]
    public ActionResult<Last24HoursDto> GetLast24HoursAverage(string symbol)
    {
        try
        {
            var result = _calculatorService.CalculateLast24HoursAverage(Common.SYMBOL_IDS[symbol.ToLower()]);

            return Ok(result);
        }
        catch (NullReferenceException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    //GET .../api/{symbol}/SimpleMovingAverage?n={numberOfDataPoints}&p={timePeriod}&s=[startDateTime]
    [HttpGet("{symbol}/SimpleMovingAverage")]
    public ActionResult<SimpleMovingAverageDto> GetSimpleMovingAverage(string symbol, int n, string p, DateTime s)
    {
        if (string.IsNullOrEmpty(p)
            || n < 1
            || p.Length < 2)
        {
            return BadRequest();
        }
        
        try
        {
            var result = new SimpleMovingAverageDto();
            TimePeriod timePeriod = GetTimePeriod(p[p.Length - 1]);
            int time = int.Parse($"{p.Remove(p.Length - 1)}");
            if (s == DateTime.MinValue)
            {
                result = _simpleMovingAverageService.CalculateSimpleMovingAverage(symbol, n, time, timePeriod);
                return Ok(result);
            }
            result = _simpleMovingAverageService.CalculateSimpleMovingAverage(symbol, n, time, timePeriod, s);
            return Ok(result);
        }
        catch (NullReferenceException)
        {
            return BadRequest();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }

    }

    private TimePeriod GetTimePeriod(char v)
    {
        switch (v)
        {
            case 'd':
                return TimePeriod.Day;
            case 'm':
                return TimePeriod.Minute;
            case 'w':
                return TimePeriod.Week;
            default: 
                throw new NullReferenceException();
        }
    }
}
