using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace TestWPF;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartCalculation_ButtonClick(object Sender, RoutedEventArgs E)
    {
        new Thread(
            () =>
            {
                var result = LongProcessCalculation();


                Dispatcher.Invoke(() => Title = DateTime.Now.ToString());

                ResultTextBlock.Dispatcher.Invoke(
                    () =>
                    {
                        ResultTextBlock.Text = result;
                    });

                Thread.Sleep(2000);
                Application.Current.Dispatcher.BeginInvoke(() => Close());

            }).Start();
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
