using Microsoft.AspNetCore.Mvc;
using step00.Models;

namespace step00.Controllers;

[ApiController]
[Route("/")]
public class TimeController : ControllerBase
{
    [HttpGet("now/utc")]
    public TimeResponse NowUtc()
    {
        var utcTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return new TimeResponse(utcTime);
    }
}
