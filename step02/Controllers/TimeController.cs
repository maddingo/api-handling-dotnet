using Microsoft.AspNetCore.Mvc;
using step02.Models;
using System.ComponentModel.DataAnnotations;

namespace step02.Controllers;

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
    /// <returns>The current UTC time in ISO 8601 format with additional metadata</returns>
    /// <response code="200">Returns the current UTC time</response>
    [HttpGet("now/utc")]
    [ProducesResponseType(typeof(TimeResponse), StatusCodes.Status200OK)]
    public ActionResult<TimeResponse> NowUtc()
    {
        var utcNow = DateTime.UtcNow;
        var utcTime = utcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        var timestamp = ((DateTimeOffset)utcNow).ToUnixTimeSeconds();
        
        return Ok(new TimeResponse 
        { 
            TimeString = utcTime,
            Timezone = "UTC",
            Timestamp = timestamp
        });
    }

    /// <summary>
    /// Gets the current local time
    /// </summary>
    /// <returns>The current local time in ISO 8601 format with timezone information</returns>
    /// <response code="200">Returns the current local time</response>
    [HttpGet("now/local")]
    [ProducesResponseType(typeof(TimeResponse), StatusCodes.Status200OK)]
    public ActionResult<TimeResponse> NowLocal()
    {
        var localNow = DateTime.Now;
        var localTime = localNow.ToString("yyyy-MM-ddTHH:mm:ss.fffK");
        var timestamp = ((DateTimeOffset)localNow).ToUnixTimeSeconds();
        var timezone = TimeZoneInfo.Local.DisplayName;
        
        return Ok(new TimeResponse 
        { 
            TimeString = localTime,
            Timezone = timezone,
            Timestamp = timestamp
        });
    }

    /// <summary>
    /// Gets time in a specific timezone
    /// </summary>
    /// <param name="timezoneId">The timezone ID (e.g., "America/New_York", "Europe/London")</param>
    /// <returns>The current time in the specified timezone</returns>
    /// <response code="200">Returns the current time in the specified timezone</response>
    /// <response code="400">Invalid timezone ID provided</response>
    [HttpGet("now/timezone/{timezoneId}")]
    [ProducesResponseType(typeof(TimeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TimeResponse> NowInTimezone([FromRoute] string timezoneId)
    {
        try
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            var timeInZone = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            var timeString = timeInZone.ToString("yyyy-MM-ddTHH:mm:ss.fff");
            var timestamp = ((DateTimeOffset)timeInZone).ToUnixTimeSeconds();
            
            return Ok(new TimeResponse 
            { 
                TimeString = timeString,
                Timezone = timeZone.DisplayName,
                Timestamp = timestamp
            });
        }
        catch (TimeZoneNotFoundException)
        {
            return BadRequest($"Invalid timezone ID: {timezoneId}");
        }
    }
}