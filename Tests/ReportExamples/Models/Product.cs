namespace ReportExamples.Models;

public record Product(int Id, string Name, string Description, decimal Price);

public record ProductsCatalog(string Name, string Description, DateTime CreationDate);

//public class ProductsCatalog
//{
//    public string Name { get; init; }

//    public string Description { get; init; }

//    public DateTime CreationDate { get; init; }

//    public override string ToString() => $"Name = {Name} Description = {Description} CreationDate = {CreationDate}";

//    public override int GetHashCode() => HashCode.Combine(Name, Description, CreationDate);

//    public override bool Equals(object? obj)
//    {
//        if (obj is not ProductsCatalog catalog) return false;
//        if (GetType() != catalog.GetType()) return false;

//        return Name == catalog.Name
//            && Description == catalog.Description
//            && CreationDate == catalog.CreationDate;
//    }
//}