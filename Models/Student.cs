namespace SchoolSystem.Models;

public class Student 
{
    public int Id { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public int ClassId { get; set; }
    public Class Class { get; set; }
}