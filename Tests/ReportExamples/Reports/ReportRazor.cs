using RazorEngine;
using RazorEngine.Templating;
using ReportExamples.Interfaces;

namespace ReportExamples.Reports;

public class ReportRazor : IProductsReport
{
    private const string __TemplateText = @"@using System.Linq
Каталог товаров
Название: @Model.CatalogName
Описание: @Model.CatalogDescription

Количество товаров в каталоге: @Model.Products.Count()

Дата формирования: @Model.CatalogCreationTime.ToString(""dd.MM.yyyy HH:mm:ss"")

@*Полная стоимость @Model.Products.Sum(p => p.Price).ToString(""c"")*@

id    Name    Description    Price
@foreach(var product in Model.Products)
{
@product.Id    @product.Name    @product.Description    @product.Price.ToString(""c"")

}
-------------------------------------------------
Полная стоимость: @Model.TotalPrice.ToString(""c"")
";

    public string CatalogName { get; set; }

    public DateTime CreationDate { get; set; }

    public string Description { get; set; }

    public IEnumerable<(int Id, string Name, string Description, decimal Price)> Products { get; set; }

    public FileInfo Create(string ReportFilePath)
    {
        var report_file = new FileInfo(ReportFilePath);
        report_file.Delete();

        var template_text = __TemplateText;

        var result = Engine.Razor.RunCompile(template_text, "ProductsTemplate", null, new
        {
            CatalogName,
            CatalogDescription = Description,
            CatalogCreationTime = CreationDate,
            Products = Products
               .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                })
               .ToArray(),
            TotalPrice = Products.Sum(p => p.Price)
        });

        using(var writer = report_file.CreateText())
            writer.Write(result);

        report_file.Refresh();
        return report_file;
    }
}
