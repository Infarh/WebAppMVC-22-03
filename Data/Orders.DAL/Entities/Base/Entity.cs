using System.ComponentModel.DataAnnotations;

namespace Orders.DAL.Entities.Base;

public abstract class Entity
{
    public int Id { get; set; }
}