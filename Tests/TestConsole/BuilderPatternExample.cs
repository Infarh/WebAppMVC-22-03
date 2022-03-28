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

public class PlotBuilder
{
    private readonly List<(Func<double, double> f, double MinX, double MaxX, double dx, OxyColor Color, double StrokeThickness)> _Plots = new();

    public OxyColor Background { get; set; }

    public OxyColor AxisLeftMajorGridlineColor { get; set; }
    public OxyColor AxisBottomMajorGridlineColor { get; set; }

    public LineStyle AxisLeftMajorGridlineStyle { get; set; }
    public LineStyle AxisBottomMajorGridlineStyle { get; set; }

    public OxyColor AxisLeftMinorGridlineColor { get; set; }
    public OxyColor AxisBottomMinorGridlineColor { get; set; }

    public LineStyle AxisLeftMinorGridlineStyle { get; set; }
    public LineStyle AxisBottomMinorGridlineStyle { get; set; }

    public void Plot(Func<double, double> f, double MinX, double MaxX, double dx, OxyColor Color, double StrokeThickness)
    {
        _Plots.Add((f, MinX, MaxX, dx, Color, StrokeThickness));
    }

    public PlotModel CreateModel()
    {
        var model = new PlotModel
        {
            Axes =
            {
                new LinearAxis
                {
                    Position = AxisPosition.Left,
                    MajorGridlineColor = AxisLeftMajorGridlineColor,
                    MajorGridlineStyle = AxisLeftMajorGridlineStyle,
                    MinorGridlineColor = AxisLeftMinorGridlineColor,
                    MinorGridlineStyle = AxisLeftMinorGridlineStyle,
                },
                new LinearAxis
                {
                    Position = AxisPosition.Bottom,
                    MajorGridlineColor = AxisBottomMajorGridlineColor,
                    MajorGridlineStyle = AxisBottomMajorGridlineStyle,
                    MinorGridlineColor = AxisBottomMinorGridlineColor,
                    MinorGridlineStyle = AxisBottomMinorGridlineStyle,
                },
            },
            Background = Background,
        };

        foreach (var (f, min, max, dx, color, thicknes) in _Plots)
            model.Series.Add(new FunctionSeries(f, min, max, dx) { Color = color, StrokeThickness = thicknes });

        return model;
    }
}
