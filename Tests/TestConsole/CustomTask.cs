using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole;

public class CustomTask<T>
{
    private readonly Func<T> _Calculation;
    private Thread _Thread;
    private T _Result;

    public T Result
    {
        get
        {
            if (_Thread is null)
            {
                Start();
            }
            _Thread.Join();
            return _Result;
        }
    }

    public CustomTask(Func<T> Calculation) => _Calculation = Calculation;

    public void Start()
    {
        if (_Thread != null) return;
        _Thread = new Thread(Calculation);
        _Thread.IsBackground = true;
        _Thread.Start();
    }

    private void Calculation()
    {
        _Result = _Calculation();
    }
}
