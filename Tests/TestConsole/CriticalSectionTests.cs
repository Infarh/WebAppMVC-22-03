namespace TestConsole;

internal static class CriticalSectionTests
{
    public static void Run()
    {
        var threads_list = new List<Thread>();

        for (var i = 0; i < 10; i++)
        {
            var i0 = i;
            var thread = new Thread(() => Print($"Message-{i0}", 50));
            thread.IsBackground = true;
            threads_list.Add(thread);
            thread.Start();
        }

        //foreach (var thread in threads_list)
        //    thread.Start();

        Console.ReadLine();
    }

    private static void Print(string Message, int Count, int Timeout = 10)
    {
        for (var i = 0; i < Count; i++)
        {
            if (Timeout > 0)
                Thread.Sleep(Timeout);

            //Console.Write("ThreadID:{0}", Thread.CurrentThread.ManagedThreadId);
            Console.Write("ThreadID:{0} ", Environment.CurrentManagedThreadId);
            Console.Write("[{0}] ", i);
            Console.WriteLine(Message);
        }
    }
}
