using System;
using System.Windows.Input;

namespace TestWPF.Infrastructure.Commands.Base;

public abstract class Command : ICommand
{
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public abstract void Execute(object? parameter);

    public virtual bool CanExecute(object? parameter) => true;
}
