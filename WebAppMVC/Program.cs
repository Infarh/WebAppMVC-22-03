using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using WebAppMVC.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    //container.RegisterType<SqlOrderService>()
    //   .As<IOrderService>()
    //   .InstancePerLifetimeScope();

    //container.RegisterType<SqlOrderService>()
    //   .AsImplementedInterfaces();

    //container.RegisterModule<ServicesModule>();
    //container.RegisterAssemblyModules(Assembly.GetEntryAssembly()!);
    container.RegisterAssemblyModules(typeof(Program));

    //var config = new ConfigurationBuilder()
    //   .AddJsonFile("autofac.config.json", false, false)
    //   .AddXmlFile("autofac.config.xml", false, false)
    //   .Build();

    //container.RegisterModule(new ConfigurationModule(new JsonResolver<Config>()))
});

#region Конфигурация сервисов приложения

var services = builder.Services;
var configuration = builder.Configuration;

services.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

//services.AddTransient<IOrderService, SqlOrderService>();

#endregion

var app = builder.Build();

// настройка конвейера обработки запросов

app.UseMiddleware<TestMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
