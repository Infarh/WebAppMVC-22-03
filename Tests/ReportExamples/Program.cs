using System.Diagnostics;
using ReportExamples.Interfaces;
using ReportExamples.Models;
using ReportExamples.Reports;

var rnd = new Random(42);
const int products_count = 20;
var products = Enumerable.Range(1, products_count)
   //.Select(i => new Product(i, $"Product-{i}", $"Description-{i}", (decimal)(Random.Shared.NextDouble() * 1000)))
   .Select(i => new Product(i, $"Product-{i}", $"Description-{i}", (decimal)Math.Round(rnd.NextDouble() * 1000, 2)))
   .ToArray();

var catalog = new ProductsCatalog("Тестовый каталог товаров", "Некоторое описание - 12313123", DateTime.Now, products);

const string template_file = "template.docx";
IProductsReport report = new ReportWord(template_file);

const string report_file = "report.docx";
//CreateReport(catalog, report, report_file);


CreateReport(catalog, new ReportRazor(), "report.txt");

Console.ReadLine();


static void CreateReport(ProductsCatalog catalog, IProductsReport generator, string TemplateFile)
{
    generator.CatalogName = catalog.Name;
    generator.Description = catalog.Description;
    generator.CreationDate = catalog.CreationDate;

    generator.Products = catalog.Products.Select(product => (product.Id, product.Name, product.Description, product.Price));

    var report_file = generator.Create(TemplateFile);
    report_file.Execute();
}