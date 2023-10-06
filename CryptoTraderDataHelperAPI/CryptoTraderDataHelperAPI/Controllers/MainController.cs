using Microsoft.AspNetCore.Mvc;

namespace CryptoTraderDataHelperAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
      var result = new
      {
          Id = 1,
          Name = "Test",
          Time = DateTime.Now,
          List = new { one= 1, Two =  2, Three =  3}
      };

        return Ok(result);
    }
}
