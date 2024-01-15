using Letemknow.Server;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContextFactory<LEKContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));
builder.Services.AddTransient<IDeviceDetectorFactory, DeviceDetectorFactory>()
                .AddTransient<ITestDataGenerator, TestDataGenerator>();
var app = builder.Build();

var dbFactory = app.Services.GetRequiredService<IDbContextFactory<LEKContext>>();
using (var context = await dbFactory.CreateDbContextAsync())
{
    await context.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
