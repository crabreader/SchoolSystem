namespace SchoolSystem.Models
{
    public class StudentTeacherViewModel
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public ICollection<string> TeacherNames { get; set; }
    }
}
