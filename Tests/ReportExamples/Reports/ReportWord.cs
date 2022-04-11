using ReportExamples.Interfaces;
using TemplateEngine.Docx;

namespace ReportExamples.Reports;

public class ReportWord : IProductsReport
{
    private const string __FieldCatalogName = "CatalogName";
    private const string __FieldCatalogDescription = "CatalogDescription";
    private const string __FieldCreationTime = "CreationTime";

    private readonly FileInfo _TemplateFile;

    public string CatalogName { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Description { get; set; } = null!;

    public ReportWord(string TemplateFilePath) => _TemplateFile = new(TemplateFilePath);

    public FileInfo Create(string ReportFilePath)
    {
        if (!_TemplateFile.Exists)
            throw new FileNotFoundException("Файл шаблона не найден", _TemplateFile.FullName);

        var report_file = new FileInfo(ReportFilePath);
        report_file.Delete();

        _TemplateFile.CopyTo(report_file.FullName);

        var content = new Content(
            new FieldContent(__FieldCatalogName, CatalogName),
            new FieldContent(__FieldCatalogDescription, Description),
            new FieldContent(__FieldCreationTime, CreationDate.ToString("dd.MM.yyyy HH:mm:ss")));

        using var doc = new TemplateProcessor(report_file.FullName)
           .SetRemoveContentControls(true);

        doc.FillContent(content);
        doc.SaveChanges();

        report_file.Refresh();

        return report_file;
    }
}
