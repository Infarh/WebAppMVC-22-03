using System;
using TestWPF.Infrastructure.Commands.Base;

namespace TestWPF.Infrastructure.Commands;

public class LambdaCommand : Command
{
    private readonly Action<object?> _OnExecute;
    private readonly Func<object?, bool>? _OnCanExecute;

    public LambdaCommand(Action<object?> OnExecute, Func<object?, bool>? OnCanExecute = null)
    {
        _OnExecute = OnExecute;
        _OnCanExecute = OnCanExecute;
    }

    public override void Execute(object? parameter)
    {
        _OnExecute(parameter);
    }

    public override bool CanExecute(object? parameter)
    {
        return base.CanExecute(parameter) && (_OnCanExecute?.Invoke(parameter) ?? true);
    }
}
