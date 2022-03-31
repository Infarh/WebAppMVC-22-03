using TestConsole;

//var xml_serializer = new StudentXmlSerializer();
//var bin_serializer = new StudentBinarySerializer();
//var json_serializer = new StudentJsonSerializer();

var serializer_type = StudentSerializerType.Json;
var students_manager = new StudentsManager(StudentsSerializer.Create(serializer_type));

students_manager.Add("Иванов", "Иван", "Иванович", DateTime.Now.AddYears(-18));
students_manager.Add("Петров", "Пётр", "Петрович", DateTime.Now.AddYears(-40));
students_manager.Add("Сидоров", "Сидор", "Сидорович", DateTime.Now.AddYears(-27));

var file_name = $"students.{serializer_type}";

students_manager.SaveTo(file_name);


//Console.ReadLine();