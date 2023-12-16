using Marketplace.DAL;
using Marketplace.DAL.Interfaces;
using Marketplace.DAL.Repositories;
using Marketplace.Service.Implementations;
using Marketplace.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string TypeConnection = "DB";
if (TypeConnection == "DB")
{
    // получаем строку подключения из файла конфигурации
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");

    // добавляем контекст ApplicationContext в качестве сервиса в приложение
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

    builder.Services.AddScoped<IProductRepository, ProductRepositoryDB>();
    builder.Services.AddScoped<IStoreRepository, StoreRepositoryDB>();
    builder.Services.AddScoped<IProductInStoreRepository, ProductInStoreRepositoryDB>();
} else
{
    builder.Services.AddScoped<IProductRepository, ProductRepositoryCSV>();
    builder.Services.AddScoped<IStoreRepository, StoreRepositoryCSV>();
    builder.Services.AddScoped<IProductInStoreRepository, ProductInStoreRepositoryCSV>();
}

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IProductInStoreService, ProductInStoreService>();

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
