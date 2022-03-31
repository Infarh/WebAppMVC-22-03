using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TestWPF.Infrastructure.Commands;
using TestWPF.Infrastructure.Extensions;
using TestWPF.ViewModels.Base;

namespace TestWPF.ViewModels;

public class MainWindowViewModel : ViewModel //INotifyPropertyChanged
{
    //public event PropertyChangedEventHandler? PropertyChanged;

    private string? _Title = "Главное окно программы";

    public string? Title
    {
        get => _Title;
        set => Set(ref _Title, value);
        //set
        //{
        //    if(_Title == value) return;
        //    _Title = value;
        //    OnPropertyChanged();
        //    //OnPropertyChanged(nameof(Title));
        //    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        //    //TitleChanged?.Invoke(this, EventArgs.Empty);
        //}
    }

    //public event EventHandler? TitleChanged;

    private string _Value = null!;
    public string Value { get => _Value; set => Set(ref _Value, value); }

    private ICommand? _ShowMessageCommand;

    public ICommand ShowMessageCommand => _ShowMessageCommand
        ??= new LambdaCommand(OnShowMessageCommandExecuted, CanShowMessageCommandExecute).Debug();

    private bool CanShowMessageCommandExecute(object? parameter)
    {
        if (parameter is null) return false;
        var message = parameter as string ?? parameter.ToString();
        if(message?.Length > 0) return true;
        return false;
    }

    private void OnShowMessageCommandExecuted(object? parameter)
    {
        if (parameter is null) return;

        var message = parameter as string ?? parameter.ToString();

        MessageBox.Show(message, "Сообщение из модели окна", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
