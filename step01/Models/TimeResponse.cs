using System.ComponentModel.DataAnnotations;

namespace step01.Models;

/// <summary>
/// Represents a time response containing a formatted time string
/// </summary>
public record TimeResponse
{
    /// <summary>
    /// The formatted time string in ISO 8601 format
    /// </summary>
    /// <example>2023-12-01T14:30:00.000Z</example>
    [Required]
    public required string TimeString { get; init; }
}