using Microsoft.AspNetCore.Mvc;
using step01.Models;
using System.ComponentModel.DataAnnotations;

namespace step01.Controllers;

/// <summary>
/// Controller for time-related operations
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class TimeController : ControllerBase
{
    /// <summary>
    /// Gets the current UTC time
    /// </summary>
    /// <returns>The current UTC time in ISO 8601 format</returns>
    /// <response code="200">Returns the current UTC time</response>
    [HttpGet("now/utc")]
    [ProducesResponseType(typeof(TimeResponse), StatusCodes.Status200OK)]
    public ActionResult<TimeResponse> NowUtc()
    {
        var utcTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        return Ok(new TimeResponse { TimeString = utcTime });
    }
}