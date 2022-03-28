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
        //var plotter = new PlotBuilder();
        //plotter
        //   .SetBackground(OxyColors.White)
        //   .SetAxisLeftMajorGridlineColor(OxyColors.Red)
        //   .Plot(x => Sinc(Math.PI * 2 * x), -5, 5, 0.01, OxyColors.Red, 2)
        //   .Plot(x => Math.Sin(2 * Math.PI * x), -5, 5, 0.01, OxyColors.Blue, 1)
        //   .Plot(x => Math.Cos(2 * Math.PI * x), -5, 5, 0.01, OxyColors.LimeGreen, 1)
        //    ;

        //var model = plotter.CreateModel();

        var model = new PlotBuilder()
               .SetBackground(OxyColors.White)
               .SetAxisLeftMajorGridlineColor(OxyColors.Red)
               .Plot(x => Sinc(Math.PI * 2 * x), -5, 5, 0.01, OxyColors.Red, 2)
               .Plot(x => Math.Sin(2 * Math.PI * x), -5, 5, 0.01, OxyColors.Blue, 1)
               .Plot(x => Math.Cos(2 * Math.PI * x), -5, 5, 0.01, OxyColors.LimeGreen, 1)
               .CreateModel()
            ;


        var exporter = new PngExporter(800, 600);
        var png_file = new FileInfo("sinc.png");
        using (var png = png_file.Create())
            exporter.Export(model, png);
    }
}

public class PlotBuilder
{
    private readonly List<(Func<double, double> f, double MinX, double MaxX, double dx, OxyColor Color, double StrokeThickness)> _Plots = new();

    public OxyColor Background { get; set; } = OxyColors.White;

    public OxyColor AxisLeftMajorGridlineColor { get; set; } = OxyColors.Gray;
    public OxyColor AxisBottomMajorGridlineColor { get; set; } = OxyColors.Gray;

    public LineStyle AxisLeftMajorGridlineStyle { get; set; } = LineStyle.Solid;
    public LineStyle AxisBottomMajorGridlineStyle { get; set; } = LineStyle.Solid;

    public OxyColor AxisLeftMinorGridlineColor { get; set; } = OxyColors.LightGray;
    public OxyColor AxisBottomMinorGridlineColor { get; set; } = OxyColors.LightGray;

    public LineStyle AxisLeftMinorGridlineStyle { get; set; } = LineStyle.Dash;
    public LineStyle AxisBottomMinorGridlineStyle { get; set; } = LineStyle.Dash;

    public PlotBuilder Plot(Func<double, double> f, double MinX, double MaxX, double dx, OxyColor Color, double StrokeThickness)
    {
        _Plots.Add((f, MinX, MaxX, dx, Color, StrokeThickness));
        return this;
    }

    public PlotBuilder SetAxisLeftMajorGridlineColor(OxyColor Color)
    {
        AxisLeftMajorGridlineColor = Color;
        return this;
    }

    public PlotBuilder SetAxisBottomMajorGridlineColor(OxyColor Color)
    {
        AxisBottomMajorGridlineColor = Color;
        return this;
    }

    public PlotBuilder SetAxisLeftMinorGridlineColor(OxyColor Color)
    {
        AxisLeftMinorGridlineColor = Color;
        return this;
    }

    public PlotBuilder SetAxisBottomMinorGridlineColor(OxyColor Color)
    {
        AxisBottomMinorGridlineColor = Color;
        return this;
    }

    public PlotBuilder SetAxisLeftMajorGridlineStyle(LineStyle Style)
    {
        AxisLeftMajorGridlineStyle = Style;
        return this;
    }

    public PlotBuilder SetAxisLeftMinorGridlineStyle(LineStyle Style)
    {
        AxisLeftMinorGridlineStyle = Style;
        return this;
    }

    public PlotBuilder SetAxisBottomMajorGridlineStyle(LineStyle Style)
    {
        AxisBottomMajorGridlineStyle = Style;
        return this;
    }

    public PlotBuilder SetAxisBottomMinorGridlineStyle(LineStyle Style)
    {
        AxisBottomMinorGridlineStyle = Style;
        return this;
    }

    public PlotBuilder SetBackground(OxyColor Color)
    {
        Background = Color;
        return this;
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
