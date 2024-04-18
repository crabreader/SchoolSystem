namespace SchoolSystem.Models;

public class Teacher 
{
    public int Id { get; set;}
    public string FirstName { get; set;}
    public string LastName { get; set;}
    public ICollection<Course> Courses { get; set; }
}