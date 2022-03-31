using TestConsole;
using System.Xml.Serialization;

var student = new Student
{
    Id = 1,
    LastName = "Иванов",
    FirstName = "Иван",
    Patronymic = "Иванович",
    Birthday = DateTime.Now.AddYears(-18)
};

var xml_serializer = new XmlSerializer(typeof(Student));

const string xml_file_name = "student.xml";

using (StreamWriter xml_file = File.CreateText(xml_file_name))
{
    xml_serializer.Serialize(xml_file, student);
}

//{
//    var xml_file = File.CreateText(xml_file_name);
//    try
//    {
//        xml_serializer.Serialize(xml_file, student);
//    }
//    finally
//    {
//        xml_file.Dispose();
//    }
//}


Student? student2 = null;
using (var xml_file = File.OpenText(xml_file_name))
{
    student2 = (Student?)xml_serializer.Deserialize(xml_file);
}

Console.ReadLine();