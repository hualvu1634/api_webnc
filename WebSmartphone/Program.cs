using Microsoft.EntityFrameworkCore;
using WebSmartphone.Data;
using WebSmartphone.Service;
using WebSmartphone.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=smartphone_store_db;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddScoped<AuthService, AuthServiceImpl>();
builder.Services.AddScoped<CategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<ProductService, ProductServiceImpl>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();