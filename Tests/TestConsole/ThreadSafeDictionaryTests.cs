using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace TestConsole;

internal static class ThreadSafeDictionaryTests
{
    public static void Run()
    {
        var strings = Enumerable.Range(1, 1000).Select(i => $"String-{i % 54}");

        foreach (var str in strings)
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var hash = ComputeHashBuffered(str);
                Console.WriteLine("{0} : {1}", str, Convert.ToBase64String(hash));
            });

        Console.ReadLine();
    }

    private static readonly ConcurrentDictionary<string, byte[]> __HashBuffer = new();

    private static byte[] ComputeHashBuffered(string Str) => __HashBuffer.GetOrAdd(Str, s => ComputeHash(s));

    private static byte[] ComputeHash(string Str)
    {
        if (Str is null) throw new ArgumentNullException(nameof(Str));
        if (Str.Length == 0) return new byte[32];

        Thread.Sleep(1000);

        var bytes = Encoding.UTF8.GetBytes(Str);
        using var hasher = MD5.Create();
        return hasher.ComputeHash(bytes);
    }
}
