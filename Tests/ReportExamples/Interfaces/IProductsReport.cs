namespace ReportExamples.Interfaces;

public interface IProductsReport
{
    string CatalogName { get; set; }

    DateTime CreationDate { get; set; }

    string Description { get; set; }

    IEnumerable<(int Id, string Name, string Description, decimal Price)> Products { get; set; }

    FileInfo Create(string ReportFilePath);
}
