using System;
using System.Collections.Generic;
using System.IO;
using TestWPF.Models;
using TestWPF.Services.Interfaces;

namespace TestWPF.Services;

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