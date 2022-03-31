using System.Windows;
using TestWPF.Infrastructure.Commands.Base;

namespace TestWPF.Infrastructure.Commands;

public class ShowMessageCommand : Command
{
    public override void Execute(object? parameter)
    {
        if (parameter is null) return;

        var message = parameter as string ?? parameter.ToString();

        MessageBox.Show(message, "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter is null) return false;
        var message = parameter as string ?? parameter.ToString();
        if (message?.Length > 0) return true;
        return false;
    }
}
