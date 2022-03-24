namespace TestConsole;

internal class AsyncAwaitTests
{
    public static void Run()
    {
        //Task task = new Task(() => PrintMessage("123")); // так делать не надо!
        //task.Start();

        var print_message_task = Task.Run(() => PrintMessage("123"));
        var another_task = print_message_task.ContinueWith(t => Console.WriteLine("Задача была завершена"));
        another_task.ContinueWith(t => Console.WriteLine("Теперь точно завершили печатать текст сообщения?!"));

        //print_message_task.Status == 
        //print_message_task.CreationOptions == 

        try
        {
            print_message_task.Wait(); // неправильно!!!
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var process_message_task = Task.Run(() => ProcessMessage("123123213"));

        try
        {
            var processing_result = process_message_task.Result; // неправильно!
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var long_runing_task = Task.Factory.StartNew(() => PrintMessage("321321321"), TaskCreationOptions.LongRunning);
    }

    private static int ProcessMessage(string Message)
    {
        Console.WriteLine($"Обработка сообщения {Message}...");
        Thread.Sleep(10);
        Console.WriteLine($"Обработка сообщения {Message} завершена.");

        return Message.Length;
    }

    //private static Task<int> ProcessMessageAsync(string Message)
    //{
    //    Console.WriteLine($"Обработка сообщения {Message}...");
    //    Thread.Sleep(10);
    //    Console.WriteLine($"Обработка сообщения {Message} завершена.");

    //    return Task.FromResult(Message.Length);
    //}

    private static async Task<int> ProcessMessageAsync(string Message)
    {
        //// стартуем в исходном потоке
        //await Task.Yield();
        //// продолжаем в потоке из пула (не всегда!)

        Console.WriteLine($"Обработка сообщения {Message}...");
        //Thread.Sleep(10); // в асинхронных методах этого быть не должно!!!
        await Task.Delay(10).ConfigureAwait(false); // false - продолжение будет выполнено в одном из потоков пула потоков
        //await Task.Yield();
        Console.WriteLine($"Обработка сообщения {Message} завершена.");

        return Message.Length;
    }

    private static void PrintMessage(string Message, int Timeout = 10)
    {
        Console.WriteLine($"Обработка сообщения {Message}...");
        if(Timeout > 0)
            Thread.Sleep(Timeout);
        Console.WriteLine($"Обработка сообщения {Message} завершена.");
    }

    public static async Task RunAsync()
    {
        var message = "123";

        var process_message_task = Task.Run(() => ProcessMessage(message));

        Console.WriteLine("Код, выполняемый в главном потоке после запуска задачи");

        try
        {
            //await process_message_task; // правильная реализация ожидания завершения задачи вместо process_message_task.Wait()
            var result = await process_message_task; // правильная реализация получения значения задачи var result = process_message_task.Result;

            Console.WriteLine($"Результат выполнения задачи составил {result}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static async Task Run2Async()
    {
        var messages = Enumerable.Range(1, 300).Select(i => $"Message-{i}");

        //var tasks_list = new List<Task<int>>();
        //foreach (var message in messages)
        //    tasks_list.Add(Task.Run(() => ProcessMessage(message)));

        ////SynchronizationContext.SetSynchronizationContext(new SynchronizationContext()); 

        //var result = await Task.WhenAll(tasks_list);

        //var result = await Task.WhenAll(messages.Select(m => Task.Run(() => ProcessMessage(m))));
        var result = await Task.WhenAll(messages.Select(m => ProcessMessageAsync(m)));

        Console.WriteLine($"Сумма = {result.Sum()}");
    }
}
