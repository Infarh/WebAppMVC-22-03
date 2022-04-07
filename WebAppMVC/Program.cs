using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using WebAppMVC.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Конфигурация сервисов приложения

var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

#endregion

var app = builder.Build();

// настройка конвейера обработки запросов

app.UseMiddleware<TestMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
