using System.ComponentModel.DataAnnotations;
using MathCore.MathParser;

using OxyPlot.Series;
using OxyPlot;

namespace TestConsole.BehaviorsPatterns.ChainsOfResponsibility;

public interface IDataProcessBlock
{
    object Process(object obj);
}

public class FunctionProcessor
{
    private List<IDataProcessBlock> _Blocks = new();

    public FunctionProcessor Add(IDataProcessBlock Block)
    {
        _Blocks.Add(Block);
        return this;
    }

    public object Process(string Function)
    {
        object value = Function;

        foreach (var block in _Blocks)
            value = block.Process(value);

        return value;
    }
}

class FunctionParser : IDataProcessBlock
{
    private readonly ExpressionParser _Parser = new();

    public object Process(object obj)
    {
        var str = (string)obj;

        var expression = _Parser.Parse(str);
        return expression;
    }
}

class ExpressionCompiller : IDataProcessBlock
{
    public object Process(object obj)
    {
        var expression = (MathExpression)obj;
        var func = expression.Compile<Func<double, double>>();
        return func;
    }
}

class PlotModelGenerator : IDataProcessBlock
{
    public object Process(object obj)
    {
        var func = (Func<double, double>)obj;

        var model = new PlotModel
        {
            Background = OxyColors.White,
            Series =
            {
                new FunctionSeries(func, -3, 3, 0.01) { Color = OxyColors.Red }
            }
        };

        return model;
    }
}

class Exporter : IDataProcessBlock
{
    private IPlotterStrategy _ExportToFile;

    public Exporter(IPlotterStrategy ExportToFile) { _ExportToFile = ExportToFile; }

    public object Process(object obj)
    {
        var model = (PlotModel)obj;
        _ExportToFile.Plot(model);
        return null;
    }
}