using Orders.DAL.Entities.Base;

namespace Orders.DAL.Entities;

public class Buyer : NamedEntity
{
    public string? LastName { get; set; }

    public string? Patronymic { get; set; }

    public DateTime Birthday { get; set; }

    public string? AboutMySelf { get; set; }
}
