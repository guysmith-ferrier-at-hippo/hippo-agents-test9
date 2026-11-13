using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Hippo.Agents.IntegrationTests;

public class HelloWorldEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private const string SecretKey = "hippo-agents-dev-jwt-super-secret-key-at-least-32-chars-long!";
    private const string Issuer = "Hippo.Agents";
    private const string Audience = "Hippo.Agents.Clients";

    public HelloWorldEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:SecretKey", SecretKey },
                    { "Jwt:Issuer", Issuer },
                    { "Jwt:Audience", Audience }
                });
            });
        });
    }

    private static string GenerateJwtToken(string username = "testuser", int expireMinutes = 60)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, "12345")
        };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [Fact]
    public async Task GetHelloWorld_WithValidToken_Returns200AndGreetingMessageUnder200Ms()
    {
        // Arrange (TC-01)
        var client = _factory.CreateClient();
        var token = GenerateJwtToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Warm up the TestHost pipeline to exclude cold start / JIT overhead from latency measurement
        await client.GetAsync("/api/v1/hello-world");

        var stopwatch = Stopwatch.StartNew();

        // Act
        var response = await client.GetAsync("/api/v1/hello-world");

        // Assert
        stopwatch.Stop();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");

        var contentString = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(contentString);
        var message = jsonDoc.RootElement.GetProperty("message").GetString();
        message.Should().Be("Hello, World!");

        stopwatch.ElapsedMilliseconds.Should().BeLessThan(200, "endpoint must respond within 200 milliseconds");
    }

    [Fact]
    public async Task GetHelloWorld_WithoutToken_Returns401Unauthorized()
    {
        // Arrange (TC-02)
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/hello-world");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetHelloWorld_WithInvalidToken_Returns401Unauthorized()
    {
        // Arrange (TC-03 - Malformed token)
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-malformed-token");

        // Act
        var response = await client.GetAsync("/api/v1/hello-world");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetHelloWorld_WithExpiredToken_Returns401Unauthorized()
    {
        // Arrange (TC-03 - Expired token)
        var client = _factory.CreateClient();
        var expiredToken = GenerateJwtToken(expireMinutes: -10);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", expiredToken);

        // Act
        var response = await client.GetAsync("/api/v1/hello-world");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task PostHelloWorld_Returns405MethodNotAllowed()
    {
        // Arrange (TC-04)
        var client = _factory.CreateClient();
        var token = GenerateJwtToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await client.PostAsync("/api/v1/hello-world", new StringContent(""));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
    }

    [Fact]
    public async Task GetHelloWorld_WhenRateLimitExceeded_Returns429TooManyRequests()
    {
        // Arrange
        using var isolatedFactory = _factory.WithWebHostBuilder(_ => { });
        var client = isolatedFactory.CreateClient();
        var token = GenerateJwtToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Exhaust the 100 permit limit
        for (var i = 0; i < 100; i++)
        {
            var allowedResponse = await client.GetAsync("/api/v1/hello-world");
            allowedResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Act
        var rateLimitedResponse = await client.GetAsync("/api/v1/hello-world");

        // Assert
        rateLimitedResponse.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
    }

    [Fact]
    public async Task GetHelloWorld_WithoutJwtSecretKey_Returns500InternalServerError()
    {
        // Arrange
        using var customFactory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "Jwt:SecretKey", null }
                });
            });
        });

        var client = customFactory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/hello-world");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("JWT secret key is missing.");
    }
}
