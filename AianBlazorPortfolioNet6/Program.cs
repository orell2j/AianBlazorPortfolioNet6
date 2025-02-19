using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using AianBlazorPortfolioNet6.Components.Data;
using AianBlazorPortfolioNet6.Components.Services;
using AianBlazorPortfolioNet6.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

builder.Services.AddDbContext<TestimonialDbContext>(options =>
    options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration)));

builder.Services.AddScoped<TestimonialService>();

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

using (var scope = app.Services.CreateScope())
{
    await DataHelper.ManageDataAsync(scope.ServiceProvider);
}

app.Run();
