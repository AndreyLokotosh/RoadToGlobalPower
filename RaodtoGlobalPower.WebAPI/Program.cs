﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RaodtoGlobalPower.Domain.Interfaces;
using RaodtoGlobalPower.Infrastructure.Data;
using RaodtoGlobalPower.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Настройка сервисов
builder.Services.AddControllers();

// Настройка подключения к базе данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозитория в DI контейнер
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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