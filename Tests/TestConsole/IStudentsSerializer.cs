using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;

namespace TestConsole;

public interface IStudentsSerializer
{
    void Serialize(Stream stream, List<Student> Students);

    List<Student> Deserialize(Stream stream);
}

public enum StudentSerializerType
{
    XML,
    Json,
    Bin,
}

public abstract class StudentsSerializer : IStudentsSerializer
{
    public static StudentsSerializer XML() => new StudentXmlSerializer();
    public static StudentsSerializer Json() => new StudentJsonSerializer();
    public static StudentsSerializer Binary() => new StudentBinarySerializer();

    public static StudentsSerializer Create(StudentSerializerType type = StudentSerializerType.Json) => type switch
    {
        StudentSerializerType.XML => XML(),
        StudentSerializerType.Json => Json(),
        StudentSerializerType.Bin => Binary(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
    };

    public abstract void Serialize(Stream stream, List<Student> Students);

    public abstract List<Student> Deserialize(Stream stream);

}

internal class StudentXmlSerializer : StudentsSerializer
{
    private static readonly XmlSerializer __Serializer = new(typeof(List<Student>));

    public override void Serialize(Stream stream, List<Student> Students)
    {
        __Serializer.Serialize(stream, Students);
    }

    public override List<Student> Deserialize(Stream stream)
    {
        return (List<Student>?)__Serializer.Deserialize(stream) 
            ?? throw new InvalidOperationException("Не удалось получить объект класса Список студентов");
    }
}

internal class StudentBinarySerializer : StudentsSerializer
{
    private static readonly BinaryFormatter __Serializer = new();

    public override void Serialize(Stream stream, List<Student> Students)
    {
        __Serializer.Serialize(stream, Students);
    }

    public override List<Student> Deserialize(Stream stream)
    {
        return (List<Student>?)__Serializer.Deserialize(stream)
            ?? throw new InvalidOperationException("Не удалось получить объект класса Список студентов");
    }
}

internal class StudentJsonSerializer : StudentsSerializer
{
    public override void Serialize(Stream stream, List<Student> Students)
    {
        JsonSerializer.Serialize(stream, Students);
    }

    public override List<Student> Deserialize(Stream stream)
    {
        return JsonSerializer.Deserialize<List<Student>>(stream)
            ?? throw new InvalidOperationException("Не удалось получить объект класса Список студентов");
    }
}