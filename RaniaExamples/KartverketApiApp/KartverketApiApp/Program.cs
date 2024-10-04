using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using KartverketApiApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register HttpClient for KommuneInfoService and StedsnavnService
// Kan implementere interface som god praksis (Anbefaling fra Espen)
builder.Services.AddHttpClient<KommuneInfoService>(client =>
{
    client.BaseAddress = new Uri("https://api.kartverket.no/kommuneinfo/v1/");
});

builder.Services.AddHttpClient<StedsnavnService>(client =>
{
    client.BaseAddress = new Uri("https://api.kartverket.no/stedsnavn/v1/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
