using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TInfrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherDBContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IVariableRepository, VariableRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

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
app.UseCors(options => options.AllowAnyOrigin());
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html"); ;
app.Run();
