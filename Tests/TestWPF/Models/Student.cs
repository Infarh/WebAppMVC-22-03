using System;

namespace TestWPF.Models;

public class Student
{
    public int Id { get; set; }

    public string LastName { get; set; }
    
    public string FirstName { get; set; }
    
    public string Patronymic { get; set; }
    
    public DateTime Birthday { get; set; }
}
