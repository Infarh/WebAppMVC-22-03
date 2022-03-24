using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TestWPF;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CancelCalculation_ButtonClick(object Sender, RoutedEventArgs E)
    {

    }

    private async void StartCalculation_ButtonClick(object Sender, RoutedEventArgs E)
    {
        if (Sender is not Button start_button) return;

        start_button.IsEnabled = false;
        CancelButton.IsEnabled = true;
        
        ResultTextBlock.Text = await Task.Run(() => LongProcessCalculation());

        CancelButton.IsEnabled = false;
        start_button.IsEnabled = true;
    }

    private string LongProcessCalculation(int Timeout = 100)
    {
        if (Timeout > 0)
            for (var i = 0; i < 100; i++)
            {
                Thread.Sleep(Timeout);
            }

        return DateTime.Now.ToString();
    }


}
