using Microsoft.AspNetCore.Identity;

using WebAppMVC.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<TestMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
