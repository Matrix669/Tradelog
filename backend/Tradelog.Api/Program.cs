using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Tradelog.Api.Data;

var builder = WebApplication.CreateBuilder(args);
const string frontendCorsPolicy = "Frontend";
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));

    // Dane testowe tylko w dev. Uruchamiane przez Migrate() / dotnet ef database update.
    if (builder.Environment.IsDevelopment())
    {
        options.UseSeeding((context, _) => DevSeedData.Seed(context));
        options.UseAsyncSeeding((context, _, cancellationToken) =>
            DevSeedData.SeedAsync(context, cancellationToken));
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: frontendCorsPolicy,
        policy =>
        {
            policy.WithOrigins(allowedOrigins);
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
    
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(frontendCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();