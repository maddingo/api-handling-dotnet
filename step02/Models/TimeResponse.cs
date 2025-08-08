using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace step02.Models;

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
    [JsonPropertyName("timeString")]
    public required string TimeString { get; init; }

    /// <summary>
    /// The timezone of the returned time
    /// </summary>
    /// <example>UTC</example>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    /// <summary>
    /// Unix timestamp representation
    /// </summary>
    /// <example>1701435000</example>
    [JsonPropertyName("timestamp")]
    public long? Timestamp { get; init; }
}