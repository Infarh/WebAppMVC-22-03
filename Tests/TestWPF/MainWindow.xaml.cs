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

    private CancellationTokenSource? _ProcessingCancellation;
    private void CancelCalculation_ButtonClick(object Sender, RoutedEventArgs E)
    {
        _ProcessingCancellation?.Cancel();
    }

    private async void StartCalculation_ButtonClick(object Sender, RoutedEventArgs E)
    {
        if (Sender is not Button start_button) return;

        var thread_id = Thread.CurrentThread.ManagedThreadId;
            
        start_button.IsEnabled = false;
        CancelButton.IsEnabled = true;

        IProgress<double> progress = new Progress<double>(p => ProgressInformer.Value = p * 100);

        var cancellation_source = new CancellationTokenSource();
        _ProcessingCancellation = cancellation_source;

        //ResultTextBlock.Text = await Task.Run(() => LongProcessCalculation());
        try
        {
            var thread_id1 = Thread.CurrentThread.ManagedThreadId;

            var result = await LongProcessCalculationAsync(20, progress, cancellation_source.Token)
               .ConfigureAwait(true);

            var thread_id2 = Thread.CurrentThread.ManagedThreadId;

            ResultTextBlock.Text = result;
        }
        catch (OperationCanceledException )
        {
            progress.Report(0);
            ResultTextBlock.Text = "Операция была отменена";
        }

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

    private static async Task<string> LongProcessCalculationAsync(int Timeout = 100, IProgress<double>? Progress = null, CancellationToken Cancel = default)
    {
        var thread_id = Thread.CurrentThread.ManagedThreadId;

        Cancel.ThrowIfCancellationRequested();
        
        const int iterations_count = 100;
        if (Timeout > 0)
            for (var i = 0; i < iterations_count; i++)
            {
                if (Cancel.IsCancellationRequested)
                {
                    // очистить ресурсы
                    Cancel.ThrowIfCancellationRequested();
                }

                var thread_id2 = Thread.CurrentThread.ManagedThreadId;


                await Task.Delay(Timeout).ConfigureAwait(false);
                //Thread.Sleep(Timeout);

                var thread_id3 = Thread.CurrentThread.ManagedThreadId;

                Progress?.Report((double)i / iterations_count);
            }

        Progress?.Report(1);

        Cancel.ThrowIfCancellationRequested();

        var thread_id4 = Thread.CurrentThread.ManagedThreadId;

        return DateTime.Now.ToString();
    }


}
