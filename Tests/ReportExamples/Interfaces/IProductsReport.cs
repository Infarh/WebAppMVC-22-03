namespace ReportExamples.Interfaces;

public interface IProductsReport
{
    string CatalogName { get; set; }

    DateTime CreationDate { get; set; }

    string Description { get; set; }

    FileInfo Create(string ReportFilePath);
}
