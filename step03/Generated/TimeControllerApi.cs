using System.Text.Json.Serialization;

namespace step03;

public interface ITimeControllerApi
{
    Task<TimeResponse> NowUtcAsync();
}

public partial class TimeControllerApi : ITimeControllerApi
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public TimeControllerApi(HttpClient httpClient, string baseUrl = "")
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = baseUrl;
    }

    public async Task<TimeResponse> NowUtcAsync()
    {
        var url = $"{_baseUrl}/Time/now/utc";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var json = await response.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<TimeResponse>(json, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        }) ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}

public class TimeResponse
{
    [JsonPropertyName("timeString")]
    public string TimeString { get; set; } = string.Empty;
}