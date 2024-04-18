namespace SchoolSystem.Models;

public class Course 
{
    public int Id { get; set;}
    public string CourseName { get; set;}
    public ICollection<Class>? Classes { get; set; }
    public ICollection<Teacher>? Teachers { get; set; }
}