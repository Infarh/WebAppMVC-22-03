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

    private static void PrintMessage(string Message, int Timeout = 10)
    {
        Console.WriteLine($"Обработка сообщения {Message}...");
        if(Timeout > 0)
            Thread.Sleep(Timeout);
        Console.WriteLine($"Обработка сообщения {Message} завершена.");
    }
}
