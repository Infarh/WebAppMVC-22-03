using System.Collections.Generic;
using System.IO;
using TestWPF.Models;

namespace TestWPF.Services.Interfaces;

public interface IStudentsSerializer
{
    void Serialize(Stream stream, List<Student> Students);

    List<Student> Deserialize(Stream stream);
}