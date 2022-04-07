
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Orders.DAL.Context;
using Orders.DAL.Entities;

#region Использование БД без контейнера сервисов
var db_options = new DbContextOptionsBuilder<OrdersDB>()
.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Orders-db");

using (var db = new OrdersDB(db_options.Options))
{
    db.Database.EnsureCreated();

    if (!db.Buyers.Any())
    {
        db.Buyers.Add(
            new Buyer
            {
                LastName = "Иванов",
                Name = "Иван",
                Patronymic = "Иванович",
                Birthday = DateTime.Now.AddYears(-18),
            });
        db.Buyers.Add(
            new Buyer
            {
                LastName = "Петров",
                Name = "Пётр",
                Patronymic = "Петрович",
                Birthday = DateTime.Now.AddYears(-13),
            });
        db.Buyers.Add(
            new Buyer
            {
                LastName = "Сидоров",
                Name = "Сидор",
                Patronymic = "Сидорович",
                Birthday = DateTime.Now.AddYears(-22),
            });

        db.SaveChanges();
    }
} 
#endregion

var services_builder = new ServiceCollection();

//services_builder.AddSingleton<IService, ServiceImplementation>();

services_builder.AddDbContext<OrdersDB>(opt => opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Orders-db"));

var service_provider = services_builder.BuildServiceProvider();

var service_db = service_provider.GetRequiredService<OrdersDB>();

foreach (var buyer in service_db.Buyers)
    Console.WriteLine("{0} {1} {2} - {3}", buyer.LastName, buyer.Name, buyer.Patronymic, buyer.Birthday);

Console.ReadLine();
