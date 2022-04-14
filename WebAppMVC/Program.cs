using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using WebAppMVC.Infrastructure.Middleware;
using WebAppMVC.Services;
using WebAppMVC.Services.Interfaces;

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

    //container.RegisterModule(new ConfigurationModule(config));
});

#region Конфигурация сервисов приложения

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllersWithViews();

services.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));

//services.AddTransient<IOrderService, SqlOrderService>();
//services.AddTransient<IEmployeesStore, InMemoryEmployesStore>();
//services.AddScoped<IEmployeesStore, InMemoryEmployesStore>();
services.AddSingleton<IEmployeesStore, InMemoryEmployesStore>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

// настройка конвейера обработки запросов

//app.UseMiddleware<TestMiddleware>();
//app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();

app.Run();
