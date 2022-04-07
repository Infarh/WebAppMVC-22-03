using System.ComponentModel.DataAnnotations;
using Orders.DAL.Entities.Base;

namespace Orders.DAL.Entities;

public class OrderItem  : Entity
{
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    [Required]
    public Order Order { get; set; } = null!;
}
