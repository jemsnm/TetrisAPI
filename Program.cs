using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TetrisAPI.Data;
using TetrisAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBContext>(options => 
    options.UseMySQL(builder.Configuration.GetConnectionString("Default")),
    contextLifetime: ServiceLifetime.Singleton,
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IStatisticsService, StatisticsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
