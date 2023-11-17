using AZ204.WebApp.Services;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conenctionString = "Endpoint=https://appconfig10005.azconfig.io;Id=QmsZ;Secret=d1OwMzZrhEi6sMzSdm6/uxk0vpPAwILK7uc9yjAGYX8=";
builder.Host.ConfigureAppConfiguration(builder =>
    builder.AddAzureAppConfiguration(option => 
        option.Connect(conenctionString).UseFeatureFlags()
    )
);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddFeatureManagement();

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
