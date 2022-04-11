namespace ReportExamples.Models;

public record ProductsCatalog(string Name, string Description, DateTime CreationDate, IEnumerable<Product> Products);

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