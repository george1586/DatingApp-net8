using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WeatherForecastController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult> GetUserFlag()
    {
        return Ok("iTEC{Th1s_1s_ez_Fl4g}");
    }
}
