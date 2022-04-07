using Microsoft.EntityFrameworkCore;
using Orders.DAL.Context;
using Orders.DAL.Entities;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Services;

public class SqlOrderService : IOrderService
{
    private readonly OrdersDB _db;
    private readonly ILogger<SqlOrderService> _Logger;

    public SqlOrderService(OrdersDB db, ILogger<SqlOrderService> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task<Order> CreateAsync(string BuyerName, string Address, string Phone, IEnumerable<(Product Product, int Quantity)> Products)
    {
        var buyer = await _db.Buyers.FirstOrDefaultAsync(b => b.Name == BuyerName);
        if (buyer is null)
        {
            await _db.Buyers.AddAsync(buyer = new Buyer
            {
                Name = BuyerName, 
                Birthday = DateTime.Now
            });
            await _db.SaveChangesAsync();
        }

        var order = new Order
        {
            Buyer = buyer,
            Address = Address,
            Phone = Phone,
            Date = DateTime.Now,
            Items = Products.Select(p => new OrderItem
                {
                    Product = p.Product,
                    Quantity = p.Quantity,
                })
               .ToArray()
        };

        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();

        return order;
    }
}
