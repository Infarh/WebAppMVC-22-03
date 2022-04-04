using System.Threading.Tasks;

namespace TestConsole.BehaviorsPatterns.ChainsOfResponsibility;

public static class ChainsOfResponsibilityExample
{
    public static void Run()
    {
        const string file_name = "ChainsOfResponsibilityExample.png";
        var processor = new FunctionProcessor()
           .Add(new FunctionParser())
           .Add(new ExpressionCompiller())
           .Add(new PlotModelGenerator())
           .Add(new Exporter(new PlotToPng(file_name)));

        const string function = "sin(x)+5*cos(2*x)-3";
        processor.Process(function);
    }
}
