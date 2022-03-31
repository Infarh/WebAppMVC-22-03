using System.Xml.Serialization;

namespace TestConsole;

public class StudentsManager
{
    private int _LastFreeId = 1;
    private List<Student> _Students = new();

    public Student Add(string LastName, string FirstName, string Patronymic, DateTime Birthday)
    {
        var student = new Student
        {
            Id = _LastFreeId++,
            LastName = LastName,
            FirstName = FirstName,
            Patronymic = Patronymic,
            Birthday = Birthday,
        };

        _Students.Add(student);

        return student;
    }

    public FileInfo SaveTo(string FilePath)
    {
        var file = new FileInfo(FilePath);

        var xml_serializer = new XmlSerializer(typeof(List<Student>));

        using (var xml = file.CreateText())
            xml_serializer.Serialize(xml, _Students);

        return file;
    }
}
