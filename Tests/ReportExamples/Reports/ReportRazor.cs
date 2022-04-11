using ReportExamples.Interfaces;

namespace ReportExamples.Reports;

public class ReportRazor : IProductsReport
{
    public string CatalogName { get; set; }

    public DateTime CreationDate { get; set; }

    public string Description { get; set; }

    public IEnumerable<(int Id, string Name, string Description, decimal Price)> Products { get; set; }

    public FileInfo Create(string ReportFilePath)
    {
        var report_file = new FileInfo(ReportFilePath);
        report_file.Delete();



        report_file.Refresh();
        return report_file;
    }
}
