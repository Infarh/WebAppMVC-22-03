namespace TestConsole;

public static class ThreadPoolTests
{
    public static void Run()
    {
        var messages = Enumerable.Range(1, 1000)
           .Select(i => $"Message-{i}");

        //ThreadPool.SetMinThreads(4, 4);
        //ThreadPool.SetMaxThreads(32, 32);
        foreach (var message in messages)
            ThreadPool.QueueUserWorkItem(_ => ProcessMessage(message));

        Console.ReadLine();
    }

    private static void ProcessMessage(string Message, int Timeout = 1000)
    {
        Console.WriteLine($"[Tid:{Environment.CurrentManagedThreadId}] Запуск процесса обработки {Message}...");
        if (Timeout > 0)
            Thread.Sleep(Timeout);
        Console.WriteLine($"[Tid:{Environment.CurrentManagedThreadId}] Процесса обработки {Message} завершён.");
    }
}
