using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TestConsole;

public static class LoggingExamples
{
    public static void Run()
    {
        var log_file_path = $"TestConsole[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log";
        var logger = new CombineLogger(
            ConsoleLogger.Logger,
            FileLogger.Create(log_file_path));

        var file_logger = FileLogger.Create(log_file_path);

        var first_file_logger = logger.Loggers.OfType<FileLogger>().First();

        //if(ReferenceEquals(file_logger, first_file_logger))
        //    logger.Log("Файловый логгер был создан в единичном экземпляре");

        //logger.Log("123");
        //logger.Log("qwe");

        var messages = Enumerable.Range(1, 300).Select(i => $"Message-{i}");

        var file_loggers_factory = new FileLoggerFactory();
        var debug_loggers_factory = new DebugLoggerFactory();
        debug_loggers_factory.OutTo = DebugLoggerFactory.LoggerType.Console;
        var rnd = new Random();

        var processor = new MessagesProcessor(rnd.NextDouble() > 0.5 ? file_loggers_factory : debug_loggers_factory);
        processor.Process(messages);
    }

    private class MessagesProcessor
    {
        private readonly ILoggerFactory _Loggers;

        public MessagesProcessor(ILoggerFactory Loggers) => _Loggers = Loggers;

        public void Process(IEnumerable<string> Messages)
        {
            var logger = _Loggers.Create();
            foreach (var message in Messages)
                logger.Log(message);
        }
    }
}

internal interface ILogger
{
    void Log(string Message);
}

internal abstract class Logger : ILogger
{
    public abstract void Log(string Message);
}

internal class ConsoleLogger : Logger
{
    private static ConsoleLogger? __Logger;
    private static readonly object __LoggerSyncRoot = new();

    //public static ConsoleLogger Logger => __Logger ?? (__Logger = new());
    //public static ConsoleLogger Logger => __Logger ??= new();
    public static ConsoleLogger Logger
    {
        get
        {
            if (__Logger != null) return __Logger;
            lock(__LoggerSyncRoot)
            {
                if (__Logger != null) return __Logger;
                __Logger = new();
                return __Logger;
            }
        }
    }

    private static readonly Lazy<ConsoleLogger> __LazyLogger = new(() => new(), LazyThreadSafetyMode.ExecutionAndPublication);
    public static ConsoleLogger LoggerLazy => __LazyLogger.Value;

    private ConsoleLogger()
    {
        
    }

    public override void Log(string Message) => Console.WriteLine($"{DateTime.Now:yyyy.MM.ddTHH:mm:ss.ffff} - {Message}");
}


internal class DebugLogger : Logger
{
    public override void Log(string Message) => Debug.WriteLine($"{DateTime.Now:yyyy.MM.ddTHH:mm:ss.ffff} - {Message}");
}

internal class TraceLogger : Logger
{
    public override void Log(string Message) => Trace.WriteLine($"{DateTime.Now:yyyy.MM.ddTHH:mm:ss.ffff} - {Message}");
}

internal class FileLogger : Logger
{
    private static readonly ConcurrentDictionary<string, FileLogger> __Loggers = new(StringComparer.OrdinalIgnoreCase);

    public static FileLogger Create(string FileName)
    {
        return __Loggers.GetOrAdd(FileName, path => new FileLogger(path));
    }

    private readonly string _FileName;

    private FileLogger(string FileName) => _FileName = FileName;

    [MethodImpl(MethodImplOptions.Synchronized)] // блокировка потоков на уровне доступа к всему методу
    public override void Log(string Message)
    {
        File.AppendAllText(_FileName, $"{DateTime.Now:s} - {Message}{Environment.NewLine}");
    }
}

internal class CombineLogger : Logger
{
    private readonly ILogger[] _Loggers;

    public IReadOnlyList<ILogger> Loggers => _Loggers;

    public CombineLogger(params ILogger[] Loggers) => _Loggers = Loggers;

    public override void Log(string Message)
    {
        foreach (var logger in _Loggers)
            logger.Log(Message);
    }
}

internal interface ILoggerFactory
{
    ILogger Create();
}

internal abstract class LoggerFactory : ILoggerFactory
{
    public abstract ILogger Create();
}

internal class DebugLoggerFactory : LoggerFactory
{
    public enum LoggerType
    {
        Console,
        Debug,
        Trace
    }

    public LoggerType OutTo { get; set; }

    public override ILogger Create() => OutTo switch
    {
        LoggerType.Console => ConsoleLogger.Logger,
        LoggerType.Debug => new DebugLogger(),
        LoggerType.Trace => new TraceLogger(),
        _ => throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType))
    };

    //switch (OutTo)
    //{
    //    default: throw new InvalidEnumArgumentException(nameof(OutTo), (int)OutTo, typeof(LoggerType));
    //    case LoggerType.Console: return ConsoleLogger.Logger;
    //    case LoggerType.Debug: return new DebugLogger();
    //    case LoggerType.Trace: return new TraceLogger();
    //}
}

internal class FileLoggerFactory : LoggerFactory
{
    public override ILogger Create() => FileLogger.Create("test-log.log");
}