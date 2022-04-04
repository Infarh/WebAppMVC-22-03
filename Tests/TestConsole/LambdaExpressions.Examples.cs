namespace TestConsole;

public class LambdaExpressionsExamples
{
    public static void Run()
    {
        //string str = "!23";
        //int x = 42;
        //List<int> ints = new();
        //Student student = new();

        //StudentProcessor process = Process;

        //ProcessStudents(Enumerable.Empty<Student>(), Free);
        ////ProcessStudents(Enumerable.Empty<Student>(), new StudentProcessor(Free));

        //Action<Student> processor2 = Free;
        //Func<Student, int> metric = s => s.LastName.Length;

        Func<double, double> sin = x => Math.Sin(x);
        Func<double, double> cos = x => Math.Cos(x);

        Func<double, double> sum_sin_cos = Sum(sin, cos);

        var result = sum_sin_cos(Math.PI / 3);
    }

    public static Func<double, double> Sum(Func<double, double> a, Func<double, double> b)
    {
        return x => a(x) + b(x);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="student"></param>
    public delegate void StudentProcessor(Student student);

    public static void ProcessStudents(IEnumerable<Student> students, StudentProcessor processor)
    {
        foreach (var student in students)
            processor(student);
    }

    public static void Process(Student student)
    {
        Console.WriteLine(student);
    }

    public static void Free(Student student)
    {
        Console.WriteLine(student);
    }
}
