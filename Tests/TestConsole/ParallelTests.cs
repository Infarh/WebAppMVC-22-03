using System.Diagnostics;

namespace TestConsole;

internal static class ParallelTests
{
    public static void Run()
    {
        var messages = Enumerable.Range(1, 300).Select(i => $"Message-{i}");

        var timer = Stopwatch.StartNew();

        //var total_length = messages
        //   .AsParallel()
        //   .WithDegreeOfParallelism(5)
        //   //.WithCancellation()
        //   //.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
        //   //.WithMergeOptions(ParallelMergeOptions.AutoBuffered)
        //   .Select(m => ProcessMessage(m))
        //   .AsSequential()
        //   .Sum();

        //foreach (var msg in messages)
        //    total_length += ProcessMessage(msg);

        //Parallel.Invoke();
        //Parallel.For(5, 100, i => Console.WriteLine($"123 - {i}"));
        Parallel.ForEach(messages, str => ProcessMessage(str));

        timer.Stop();

        Console.WriteLine("Обработка сообщений завершена за {0}", timer.Elapsed);
    }

    private static int ProcessMessage(string Message)
    {
        Console.WriteLine($"Обработка сообщения {Message}...");
        Thread.Sleep(10);
        Console.WriteLine($"Обработка сообщения {Message} завершена.");

        return Message.Length;
    }
}
