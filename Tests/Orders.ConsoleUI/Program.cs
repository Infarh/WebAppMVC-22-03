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

    public static IServiceProvider Services => Hosting.Services;

    public static async Task Main(string[] args)
    {
        var host = Hosting;

        await host.StartAsync();

        await PrintBuyersAsync();

        Console.ReadLine();

        await host.StopAsync();
    }

    private static async Task PrintBuyersAsync()
    {
        await using var servces_scope = Services.CreateAsyncScope();
        var services = servces_scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<Program>>();
        var db = services.GetRequiredService<OrdersDB>();

        //await db.Database.EnsureCreatedAsync();
        await db.Database.MigrateAsync();

        foreach (var buyer in db.Buyers)
        {
            //Console.WriteLine(buyer.LastName);
            logger.LogInformation("Покупатель[{0}] {1} {2} {3} - {4}",
                buyer.Id, buyer.LastName, buyer.Name, buyer.Patronymic, buyer.Birthday);
        }
    }
}
