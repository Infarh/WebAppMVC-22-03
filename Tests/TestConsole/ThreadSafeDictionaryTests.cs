﻿using System.Collections.Generic;
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

        var hash_pool = new ConcurrentDictionary<string, byte[]>();

        foreach (var str in strings)
            ThreadPool.QueueUserWorkItem(_ =>
            {
                var hash = hash_pool.GetOrAdd(str, s => ComputeHash(s));
                Console.WriteLine("{0} : {1}", str, Convert.ToBase64String(hash));
            });

        Console.ReadLine();
    }

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
