namespace SchoolSystem.Models;

public class StudentTeacherCourseViewModel
{
    public Course Course { get; set; }
    public IEnumerable<Student>? Students { get; set; }
    public IEnumerable<Teacher>? Teachers { get; set; }
    public List<int>? SelectedTeachers { get; set; }
}
