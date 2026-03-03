// Minimal API Back-End (ServerApp.cs):
// This file was developed with significant assistance from GitHub Copilot (GPT-4.1).
// Copilot contributed by:
// - Suggesting the minimal API structure for .NET 6+.
// - Recommending the use of CORS for cross-origin requests between ClientApp and ServerApp.
// - Generating sample product data and the API endpoint structure.
// - Optimizing CORS configuration for development efficiency.
//
// Copilot improved efficiency by:
// - Auto-completing repetitive code blocks (e.g., product list structure).
// - Suggesting concise lambda syntax for endpoint mapping.
// - Highlighting best practices for CORS setup and API design.
//
// Areas of optimization suggested by Copilot:
// - Using anonymous types for quick prototyping of product data.
// - Streamlining CORS policy to allow all headers and methods for rapid integration testing.
// - Keeping the API minimal for fast iteration and debugging.
using Microsoft.Extensions.Caching.Memory;
var builder = WebApplication.CreateBuilder(args);
// Add in-memory caching
builder.Services.AddMemoryCache();

// Add CORS to allow requests from ClientApp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5216")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
// Use caching for product list endpoint
app.MapGet("/api/productlist", (IMemoryCache cache) =>
{
    const string cacheKey = "productlist";
    if (!cache.TryGetValue(cacheKey, out object productList))
    {
        productList = new[]
        {
            new
            {
                id = 1,
                name = "Laptop",
                price = 1200.50,
                stock = 25,
                category = new { id = 101, name = "Electronics" }
            },
            new
            {
                id = 2,
                name = "Headphones",
                price = 50.00,
                stock = 100,
                category = new { id = 102, name = "Accessories" }
            }
        };
        // Cache for 5 minutes
        cache.Set(cacheKey, productList, TimeSpan.FromMinutes(5));
    }
    return productList;
});
app.Run();