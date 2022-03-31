using System;
namespace TestConsole;

public class CustomTaskTests
{
    public static void Run()
    {
        var calculation = new CustomTask<string>(
            () =>
            {
                Console.WriteLine("Процевв вычисления запущен");
                Thread.Sleep(3000);
                Console.WriteLine("Процевв вычисления завершён");
                return "42";
            });

        calculation.Start();
        Console.WriteLine("Запустили процесс расчёта...");

        var result = calculation.Result;

        Console.WriteLine("Результат = {0}", result);

        Console.ReadLine();
    }
}