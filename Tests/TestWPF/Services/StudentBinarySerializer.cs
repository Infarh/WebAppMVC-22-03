using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TestWPF.Models;

namespace TestWPF.Services;

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