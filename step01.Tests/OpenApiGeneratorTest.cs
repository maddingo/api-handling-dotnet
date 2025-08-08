using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace step01.Tests;

public class OpenApiGeneratorTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public OpenApiGeneratorTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task SwaggerEndpoint_ReturnsOpenApiSpec()
    {
        var response = await _client.GetAsync("/swagger/v1/swagger.json");
        
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.NotEmpty(content);
        
        var jsonDocument = JsonDocument.Parse(content);
        var root = jsonDocument.RootElement;
        
        Assert.True(root.TryGetProperty("openapi", out _));
        Assert.True(root.TryGetProperty("info", out _));
        Assert.True(root.TryGetProperty("paths", out _));
    }

    [Fact]
    public async Task SwaggerSpec_ContainsTimeEndpoint()
    {
        var response = await _client.GetAsync("/swagger/v1/swagger.json");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        var jsonDocument = JsonDocument.Parse(content);
        var root = jsonDocument.RootElement;
        
        Assert.True(root.TryGetProperty("paths", out var paths));
        Assert.True(paths.TryGetProperty("/Time/now/utc", out var timeEndpoint));
        Assert.True(timeEndpoint.TryGetProperty("get", out _));
    }
}