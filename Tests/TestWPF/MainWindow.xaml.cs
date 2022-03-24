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

        IProgress<double> progress = new Progress<double>(p => ProgressInformer.Value = p * 100);

        //ResultTextBlock.Text = await Task.Run(() => LongProcessCalculation());
        ResultTextBlock.Text = await LongProcessCalculationAsync(20, progress);

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

    private async Task<string> LongProcessCalculationAsync(int Timeout = 100, IProgress<double>? Progress = null, CancellationToken Cancel = default)
    {
        const int iterations_count = 100;
        if (Timeout > 0)
            for (var i = 0; i < iterations_count; i++)
            {
                await Task.Delay(Timeout).ConfigureAwait(false);
                //Thread.Sleep(Timeout);

                Progress?.Report((double)i / iterations_count);
            }

        Progress?.Report(1);

        return DateTime.Now.ToString();
    }


}
