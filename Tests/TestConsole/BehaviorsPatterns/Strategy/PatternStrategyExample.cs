using MathCore.Functions.Differentiable;
using MathCore.MathParser;
using OxyPlot;
using OxyPlot.ImageSharp;
using OxyPlot.Series;

namespace TestConsole;

public class PatternStrategyExample
{
    public static void Run()
    {
        const string function = "sin(x)+5*cos(2*x)-3";

        //var parser = new ExpressionParser();
        //var expression = parser.Parse(function);

        //var func = expression.Compile<Func<double, double>>();

        //var model = new PlotModel
        //{
        //    Background = OxyColors.White,
        //    Series =
        //    {
        //        new FunctionSeries(func, -3, 3, 0.01) { Color = OxyColors.Red }
        //    }
        //};

        const string file_name = "function.png";
        //using (var file = File.Create(file_name))
        //{
        //    var png = new PngExporter(800, 600);
        //    png.Export(model, file);
        //}


        var plot_to_png = new PlotToPng(file_name);
        var plot_to_jpg = new PlotToJpg(file_name);

        var calculator = new FunctionCalculator(plot_to_jpg);

        calculator.PlotFunction(function);
    }
}

public class FunctionCalculator
{
    private readonly IPlotterStrategy _PlotterStrategy;
    private ExpressionParser _Parser = new();

    public FunctionCalculator(IPlotterStrategy PlotterStrategy) => _PlotterStrategy = PlotterStrategy;

    public void PlotFunction(string FunctionText)
    {
        var expression = _Parser.Parse(FunctionText);

        var func = expression.Compile<Func<double, double>>();

        var model = new PlotModel
        {
            Background = OxyColors.White,
            Series =
            {
                new FunctionSeries(func, -3, 3, 0.01) { Color = OxyColors.Red }
            }
        };

        _PlotterStrategy.Plot(model);
    }
}

public interface IPlotterStrategy
{
    public void Plot(IPlotModel Plot);
}

public class PlotToPng : IPlotterStrategy
{
    private readonly string _FilePath;
    private readonly int _Width;
    private readonly int _Height;
    private readonly double _Resolution;

    public PlotToPng(string FilePath, int Width = 800, int Height = 600, double Resolution = 96)
    {
        _FilePath = FilePath;
        _Width = Width;
        _Height = Height;
        _Resolution = Resolution;
    }

    public void Plot(IPlotModel Plot)
    {
        var file = new FileInfo(_FilePath);

        using var stream = file.Create();
        var png = new PngExporter(_Width, _Height, _Resolution);
        png.Export(Plot, stream);

        file.Execute();
    }
}
public class PlotToJpg : IPlotterStrategy
{
    private readonly string _FilePath;
    private readonly int _Width;
    private readonly int _Height;
    private readonly double _Resolution;
    private readonly int _Quality;

    public PlotToJpg(string FilePath, int Width = 800, int Height = 600, double Resolution = 96, int Quality = 75)
    {
        _FilePath = FilePath;
        _Width = Width;
        _Height = Height;
        _Resolution = Resolution;
        _Quality = Quality;
    }

    public void Plot(IPlotModel Plot)
    {
        var file = new FileInfo(_FilePath);

        using var stream = file.Create();
        var png = new JpegExporter(_Width, _Height, _Resolution, _Quality);
        png.Export(Plot, stream);

        file.Execute();
    }
}
