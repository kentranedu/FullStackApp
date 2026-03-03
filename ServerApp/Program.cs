//Minimal API Back-End (ServerApp.cs):
var builder = WebApplication.CreateBuilder(args);

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
app.MapGet("/api/productlist", () =>
{
    return new[]
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
});
app.Run();