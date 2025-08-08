using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using step02.Models;

namespace step02.Tests;

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
        Assert.Equal("UTC", timeResponse.Timezone);
        Assert.NotNull(timeResponse.Timestamp);
        Assert.True(DateTime.TryParse(timeResponse.TimeString, out _));
    }

    [Fact]
    public async Task NowLocal_ReturnsValidTimeResponse()
    {
        var response = await _client.GetAsync("/Time/now/local");
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var timeResponse = JsonSerializer.Deserialize<TimeResponse>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(timeResponse);
        Assert.NotNull(timeResponse.TimeString);
        Assert.NotNull(timeResponse.Timezone);
        Assert.NotNull(timeResponse.Timestamp);
        Assert.True(DateTime.TryParse(timeResponse.TimeString, out _));
    }

    [Fact]
    public async Task NowInTimezone_WithValidTimezone_ReturnsValidTimeResponse()
    {
        var response = await _client.GetAsync("/Time/now/timezone/UTC");
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var timeResponse = JsonSerializer.Deserialize<TimeResponse>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(timeResponse);
        Assert.NotNull(timeResponse.TimeString);
        Assert.NotNull(timeResponse.Timezone);
        Assert.NotNull(timeResponse.Timestamp);
    }

    [Fact]
    public async Task NowInTimezone_WithInvalidTimezone_ReturnsBadRequest()
    {
        var response = await _client.GetAsync("/Time/now/timezone/InvalidTimezone");
        
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task HealthCheck_ReturnsHealthy()
    {
        var response = await _client.GetAsync("/health");
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.Equal("Healthy", content);
    }
}