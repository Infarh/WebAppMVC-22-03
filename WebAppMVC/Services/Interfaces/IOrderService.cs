using Orders.DAL.Entities;

namespace WebAppMVC.Services.Interfaces;

public interface IOrderService
{
    Task<Order> CreateAsync(string BuyerName, string Address, string Phone, IEnumerable<(Product Product, int Quantity)> Products);
}
