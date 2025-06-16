using OurTracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OurTrackerContext>(options =>
    options.UseSqlite("Data Source=ourtracker.db"));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tracker}/{action=Index}/{id?}");

app.Run();