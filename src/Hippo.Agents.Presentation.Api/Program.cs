using System.Text;
using Hippo.Agents.Presentation.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"] ?? "hippo-agents-dev-jwt-super-secret-key-at-least-32-chars-long!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"] ?? "Hippo.Agents",
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"] ?? "Hippo.Agents.Clients",
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

app.MapGet("/api/v1/hello-world", () => Results.Ok(new HelloWorldResponse("Hello, World!")))
    .RequireAuthorization()
    .WithName("GetHelloWorld")
    .Produces<HelloWorldResponse>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized);

app.Run();

public partial class Program { }
