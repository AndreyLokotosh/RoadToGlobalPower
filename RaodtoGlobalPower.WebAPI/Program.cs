using System.Reflection;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Infrastructure.Data;
using RaodtoGlobalPower.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args) ?? throw new ArgumentNullException("WebApplication.CreateBuilder(args)");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new DateOnlyConverter());
});
// Настройка сервисов
builder.Services.AddControllers();

// Настройка подключения к базе данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев в DI контейнер
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAttestationRepository, AttestationRepository>(); // Добавляем AttestationRepository

// Настройка Swagger
builder.Services.AddSwaggerGen(c =>
{
    // Указываем путь к XML-файлу с комментариями
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath); // Подключаем XML-комментарии

    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Employee API", Version = "v1" });
});

var app = builder.Build();

// Использование Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Генерация swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
    });
}

// Использование маршрутов и обработки запросов
app.UseAuthorization();
app.MapControllers();

app.Run();