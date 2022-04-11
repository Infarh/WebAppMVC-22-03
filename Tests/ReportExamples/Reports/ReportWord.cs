using ReportExamples.Interfaces;

namespace ReportExamples.Reports;

public class ReportWord : IProductsReport
{
    public string CatalogName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Description { get; set; } = null!;

    public FileInfo Create(string ReportTemplateFile)
    {
        throw new NotImplementedException();
    }
}
