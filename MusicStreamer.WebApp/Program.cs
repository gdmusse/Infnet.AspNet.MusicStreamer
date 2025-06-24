using MusicStreamer.Domain.Interfaces;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Repositories;
using Microsoft.EntityFrameworkCore;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;
using MusicStreamer.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var urlApi = builder.Configuration["UriApi"];

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(urlApi); 
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
