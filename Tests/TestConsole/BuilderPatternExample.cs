using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.ImageSharp;
using OxyPlot.Series;

namespace TestConsole;

public class BuilderPatternExample
{
    public static double Sinc(double x) => x == 0 ? 1 : Math.Sin(x) / x;

    public static void Run()
    {
        var model = new PlotModel
        {
            Axes =
            {
                new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MajorGridlineColor = OxyColors.Gray,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineColor = OxyColors.LightGray,
                    MinorGridlineStyle = LineStyle.Dash,
                },
                new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MajorGridlineColor = OxyColors.Gray,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineColor = OxyColors.LightGray,
                    MinorGridlineStyle = LineStyle.Dash,
                },
            },
            Background = OxyColors.White,
            Series =
            {
                new FunctionSeries(x => Sinc(2 * Math.PI * x), -5, 5, 0.01, "Sinc(x)")
                {
                    Color = OxyColors.Red,
                    StrokeThickness = 2,
                }
            }
        };

        var exporter = new PngExporter(800, 600);
        var png_file = new FileInfo("sinc.png");
        using (var png = png_file.Create())
            exporter.Export(model, png);
    }
}
