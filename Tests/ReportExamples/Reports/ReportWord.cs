using ReportExamples.Interfaces;
using TemplateEngine.Docx;

namespace ReportExamples.Reports;

public class ReportWord : IProductsReport
{
    private const string __FieldProduct = "Product";

    private const string __FieldProductId = "ProductId";
    private const string __FieldProductName = "ProductName";
    private const string __FieldProductDescription = "ProductDescription";
    private const string __FieldProductPrice = "ProductPrice";

    private const string __FieldTotalPrice = "TotalPrice";

    private const string __FieldCatalogName = "CatalogName";
    private const string __FieldCatalogDescription = "CatalogDescription";
    private const string __FieldCreationTime = "CreationTime";

    private readonly FileInfo _TemplateFile;

    public string CatalogName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Description { get; set; } = null!;

    public IEnumerable<(int Id, string Name, string Description, decimal Price)> Products { get; set; }

    public ReportWord(string TemplateFilePath) => _TemplateFile = new(TemplateFilePath);

    public FileInfo Create(string ReportFilePath)
    {
        if (!_TemplateFile.Exists)
            throw new FileNotFoundException("Файл шаблона не найден", _TemplateFile.FullName);

        var report_file = new FileInfo(ReportFilePath);
        report_file.Delete();

        _TemplateFile.CopyTo(report_file.FullName);

        var rows = Products.Select(p => new TableRowContent(new List<FieldContent>
        {
            new (__FieldProductId, p.Id.ToString()),
            new (__FieldProductName, p.Name),
            new (__FieldProductDescription, p.Description),
            new (__FieldProductPrice, p.Price.ToString("c")),
        })).ToArray();

        var content = new Content(
            new FieldContent(__FieldCatalogName, CatalogName),
            new FieldContent(__FieldCatalogDescription, Description),
            new FieldContent(__FieldCreationTime, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")),
            TableContent.Create(__FieldProduct, rows),
            new FieldContent(__FieldTotalPrice, Products.Sum(p => p.Price).ToString("c"))
            );

        using var doc = new TemplateProcessor(report_file.FullName)
           .SetRemoveContentControls(true);

        doc.FillContent(content);
        doc.SaveChanges();

        report_file.Refresh();

        return report_file;
    }
}
