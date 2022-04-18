using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Identity.DAL.Context;
using Identity.DAL.Entities;
using Microsoft.AspNetCore.Identity;
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

services.AddIdentity<User, Role>()
   .AddEntityFrameworkStores<IdentityDB>()
   .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebAppMVC";
    opt.Cookie.HttpOnly = true;
    //opt.Cookie.Expiration = TimeSpan.FromDays(10);
    

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

services.AddControllersWithViews();

services.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));
services.AddDbContext<IdentityDB>(opt => opt.UseSqlServer(configuration.GetConnectionString("Identity")));

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

app.UseAuthentication();
app.UseAuthorization();

// настройка конвейера обработки запросов

//app.UseMiddleware<TestMiddleware>();
//app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();

app.Run();
