using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestWPF.Models;
using TestWPF.Services.Interfaces;

namespace TestWPF.Services;

public class StudentsManager
{
    private readonly IStudentsSerializer _Serializer;
    private int _LastFreeId = 1;
    private List<Student> _Students = new();

    public StudentsManager(IStudentsSerializer Serializer)
    {
        _Serializer = Serializer;
    }

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

        using (var xml = file.Create())
            _Serializer.Serialize(xml, _Students);

        return file;
    }

    public void LoadFrom(string FilePath)
    {
        using var xml = File.OpenRead(FilePath);
        _Students = _Serializer.Deserialize(xml);
        if(_Students.Count == 0) return;

        _LastFreeId = _Students.Max(student => student.Id) + 1;
    }
}
