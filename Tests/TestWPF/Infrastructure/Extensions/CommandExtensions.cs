using System.Windows.Input;
using TestWPF.Infrastructure.Commands;

namespace TestWPF.Infrastructure.Extensions;

internal static class CommandExtensions
{
    public static ICommand Debug(this ICommand Command)
    {
        if (Command is DebugCommand) return Command;
        return new DebugCommand(Command);
    }
}
