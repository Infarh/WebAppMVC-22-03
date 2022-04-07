using System.ComponentModel.DataAnnotations;
using Orders.DAL.Entities.Base;

namespace Orders.DAL.Entities;

public class Order : Entity
{
    public DateTime Date { get; set; }

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string Phone { get; set; } = null!;

    [Required]
    public Buyer Buyer { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

    public string? Description { get; set; }
}
