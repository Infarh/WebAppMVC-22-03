using System.ComponentModel.DataAnnotations;

namespace Orders.DAL.Entities.Base;

public abstract class NamedEntity : Entity
{
    [Required]
    public string Name { get; set; } = null!;
}
