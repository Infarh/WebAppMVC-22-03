namespace TestConsole;

public static class SynchronizationTests
{
    private static void MonitorTest()
    {
        var sync_root = new object();

        lock (sync_root)
        {
            Console.WriteLine("Критическая секция");
        }

        Monitor.Enter(sync_root);
        try
        {
            Console.WriteLine("Критическая секция");
        }
        finally
        {
            Monitor.Exit(sync_root);
        }

        //Monitor.TryEnter()
    }

    private static void MutexSemaphoreTest()
    {
        var mutex = new Mutex(true, "Название нашего приложения", out var is_created_new);

        var semaphore = new Semaphore(0, 8);

        semaphore.WaitOne(); // попытка пройти через семафор
        //if (!semaphore.WaitOne(3000)) // попытка блокировки с таймаутом
        //{
        //    return;
        //}

        try
        {
            Console.WriteLine("Критическая секция");
        }
        finally
        {
            semaphore.Release(); // выходим из зоны действия семафора
        }
    }

    private static void EventsTests()
    {
        var manual_event = new ManualResetEvent(false);
        var auto_event = new AutoResetEvent(false);

        var threads = new Thread[10];
        for (var i = 0; i < threads.Length; i++)
        {
            threads[i] = new Thread(() =>
            {
                var thread_id = Environment.CurrentManagedThreadId;
                Console.WriteLine("Поток {0} создан и ждёт разрешения на выполнение: {1:HH:mm:ss.fff}", thread_id, DateTime.Now);

                auto_event.WaitOne();

                Console.WriteLine("Поток {0} завершил свою работу: {1:HH:mm:ss.fff}", thread_id, DateTime.Now);

                //manual_event.Reset();
            });

            threads[i].Start();
            Thread.Sleep(100);
        }

        Console.WriteLine("Создание потоков выполнено");
        Console.ReadLine();

        Console.WriteLine("Потокам разрешено выполнение");
        auto_event.Set();
        Console.ReadLine();

        Console.WriteLine("Потокам разрешено выполнение");
        auto_event.Set();
        Console.ReadLine();

        Console.WriteLine("Потокам разрешено выполнение");
        auto_event.Set();
        Console.ReadLine();

        Console.WriteLine("Потокам разрешено выполнение");
        auto_event.Set();
        Console.ReadLine();

        Console.WriteLine("Потокам разрешено выполнение");
        auto_event.Set();

        Console.ReadLine();
    }

    public static void Run()
    {
       //MonitorTest();
       //MutexSemaphoreTest();

       EventsTests();
    }
}
