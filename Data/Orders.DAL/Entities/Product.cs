using System.ComponentModel.DataAnnotations.Schema;
using Orders.DAL.Entities.Base;

namespace Orders.DAL.Entities;

public class Product : NamedEntity
{
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public string? Category { get; set; }
}
