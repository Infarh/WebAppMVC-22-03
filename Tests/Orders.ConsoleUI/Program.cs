using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orders.DAL.Context;

namespace Orders.ConsoleUI;

class Program
{
    private static IHost? __Host;

    public static IHost Hosting => __Host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
       .ConfigureHostConfiguration(opt => opt.AddJsonFile("appsettings.json"))
       .ConfigureAppConfiguration(opt => opt
           .AddJsonFile("appsettings.json")
           .AddXmlFile("appsettings.xml", true)
           .AddIniFile("appsettings.ini", true)
           .AddEnvironmentVariables()
           .AddCommandLine(args))
       //.ConfigureLogging(opt => opt.ClearProviders().AddConsole().AddDebug())
           .ConfigureServices(ConfigureServices)
    ;

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection Services)
    {
        Services.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(host.Configuration.GetConnectionString("SqlServer")));
    }

    public static async Task Main(string[] args)
    {
        var host = Hosting;

        await host.StartAsync();

        PrintBuyers();

        Console.ReadLine();

        await host.StopAsync();
    }

    private static void PrintBuyers()
    {

    }
}
