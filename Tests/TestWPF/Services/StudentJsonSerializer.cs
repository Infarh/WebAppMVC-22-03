using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TestWPF.Models;

namespace TestWPF.Services;

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