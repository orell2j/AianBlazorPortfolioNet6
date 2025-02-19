using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using AianBlazorPortfolioNet6.Components.Data;
using AianBlazorPortfolioNet6.Components.Services;
using AianBlazorPortfolioNet6.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Log the raw environment variable for debugging
var rawDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine($"[DEBUG] Raw DATABASE_URL = {rawDatabaseUrl ?? "null"}");

// Get the connection string via our helper
var connStr = ConnectionHelper.GetConnectionString(builder.Configuration);
if (string.IsNullOrEmpty(connStr))
{
    Console.WriteLine("Connection string is null or empty!");
}
else
{
    Console.WriteLine($"Connection string: {connStr}");
}

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

// Register the EF Core DbContext using the connection string from ConnectionHelper.
builder.Services.AddDbContext<TestimonialDbContext>(options =>
    options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration)));

// Register custom services.
builder.Services.AddScoped<TestimonialService>();

// Register HttpClient in DI.
builder.Services.AddScoped<HttpClient>(sp =>
{
    var navManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(navManager.BaseUri) };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Automatically apply migrations.
using (var scope = app.Services.CreateScope())
{
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

app.Run();
