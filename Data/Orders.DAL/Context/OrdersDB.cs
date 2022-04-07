using Microsoft.EntityFrameworkCore;
using Orders.DAL.Entities;

namespace Orders.DAL.Context;

public class OrdersDB : DbContext
{
    public DbSet<Buyer> Buyers { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public OrdersDB(DbContextOptions<OrdersDB> options) : base(options)
    {
        
    }
}
