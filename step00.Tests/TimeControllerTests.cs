using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using step00.Models;

namespace step00.Tests;

public class TimeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TimeControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task NowUtc_ReturnsValidTimeResponse()
    {
        var response = await _client.GetAsync("/Time/now/utc");
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var timeResponse = JsonSerializer.Deserialize<TimeResponse>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(timeResponse);
        Assert.NotNull(timeResponse.TimeString);
        Assert.True(DateTime.TryParse(timeResponse.TimeString, out _));
    }
}