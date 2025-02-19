using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using AianBlazorPortfolioNet6.Components.Data;
using AianBlazorPortfolioNet6.Components.Services;
using AianBlazorPortfolioNet6.Helpers; // include the Helpers namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register controllers so API endpoints are available.
builder.Services.AddControllers();

// Register the EF Core database context using PostgreSQL with the connection string from ConnectionHelper.
builder.Services.AddDbContext<TestimonialDbContext>(options =>
    options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration)));

// Register your custom TestimonialService.
builder.Services.AddScoped<TestimonialService>();

// Register HttpClient in DI so it can be injected in your components.
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

// Map controllers (API endpoints)
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Perform automatic migrations at startup.
using (var scope = app.Services.CreateScope())
{
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

app.Run();
