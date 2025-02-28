using Common;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Common.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Common.Data;

var builder = WebApplication.CreateBuilder(args);
// Configure configuration sources
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())  // Not necessary since WebApplication handles this already.
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)   // Adds appsettings.json.
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)  // Adds environment-specific settings, e.g., appsettings.Development.json.
    .AddEnvironmentVariables(); // Loads environment variables

// Set the connection string
ConnectionStrings.DefaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with MySQL configuration
builder.Services.AddDbContext<EcommerceTestContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add DbContext for ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var apiKey = "CEGAChAtA7Ds40p";
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthorizationHandler, APIAuthorizationHandler>();
builder.Services.AddTransient<IAuthorizationHandler, LoginAuthorizationHandler>();
builder.Services.AddAuthorization(authConfig =>
{
    authConfig.AddPolicy("ApiPolicy",
        policyBuilder => policyBuilder
            .AddRequirements(new APIAuthorization(new[] { apiKey })));
    authConfig.AddPolicy("LoginPolicy",
        policyBuilder => policyBuilder
            .AddRequirements(new LoginAuthorization(new[] { apiKey })));
});

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add the authentication middleware
app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
