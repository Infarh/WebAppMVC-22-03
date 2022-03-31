using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestWPF.Infrastructure.Commands.Base;

namespace TestWPF.Infrastructure.Commands;

public class DebugCommand : Command
{
    private readonly ICommand _BaseCommand;

    public DebugCommand(ICommand BaseCommand) => _BaseCommand = BaseCommand;

    public override void Execute(object? parameter)
    {
        var timer = Stopwatch.StartNew();
        Debug.WriteLine("Команда {0} запущена", _BaseCommand);

        _BaseCommand.Execute(parameter);

        Debug.WriteLine("Команда {0} выполнена за {1} мс", _BaseCommand, timer.ElapsedMilliseconds);

    }

    public override bool CanExecute(object? parameter)
    {
        Debug.WriteLine("Запрос возможности выполнения команды {0}", _BaseCommand);

        var result = _BaseCommand.CanExecute(parameter);

        Debug.WriteLine("Команду {0} можно выполнить - {1}", _BaseCommand, result);

        return result;
    }
}
