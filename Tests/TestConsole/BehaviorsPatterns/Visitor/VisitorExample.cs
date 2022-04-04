using System.Linq.Expressions;

namespace TestConsole.BehaviorsPatterns.Visitor;

public static class VisitorExample
{
    public static void Run()
    {
        const string path = @"..\..\..\..\..";

        var visitot = new SourceCounterVisitor();
        var dir = new DirectoryInfo(path);
        visitot.Visit(dir);
        }
}

public class SourceCounterVisitor
{
    public int Count { get; private set; }

    public void Visit(DirectoryInfo dir)
    {
        foreach (var cs_file in dir.EnumerateFiles("*.cs"))
        {
            var count = GetLinesCount(cs_file);
            Count += count;
        }

        foreach (var directory in dir.EnumerateDirectories())
            Visit(directory);
    }

    public void Clear() => Count = 0;

    private int GetLinesCount(FileInfo file)
    {
        var count = GetLines(file)
           .Select(s => s.Trim())
           .Count(str => str.Length > 0 && !str.StartsWith("//"));
        return count;
    }

    private IEnumerable<string> GetLines(FileInfo file)
    {
        using var reader = file.OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine()!;
    }
}
