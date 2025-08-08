using Microsoft.AspNetCore.Mvc;

namespace step03.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase, ITimeControllerApi
{
    [HttpGet("now/utc")]
    public async Task<TimeResponse> NowUtcAsync()
    {
        var utcTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return new TimeResponse { TimeString = utcTime };
    }

    async Task<TimeResponse> ITimeControllerApi.NowUtcAsync()
    {
        return await NowUtcAsync();
    }
}