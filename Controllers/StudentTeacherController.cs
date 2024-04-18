using Microsoft.AspNetCore.Mvc;
using SchoolSystem.Data;
using SchoolSystem.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystem.Controllers
{
    public class StudentTeacherController : Controller
    {
        private readonly SchoolSystemContext _context;

        public StudentTeacherController(SchoolSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = _context.Student
                .Include(s => s.Class)
                .ThenInclude(c => c.Courses)
                .ThenInclude(co => co.Teachers)
                .Select(s => new StudentTeacherViewModel
                {
                    StudentId = s.Id,
                    StudentFirstName = s.FirstName,
                    StudentLastName = s.LastName,
                    TeacherNames = s.Class.Courses.SelectMany(c => c.Teachers)
                                                    .Select(t => $"{t.FirstName} {t.LastName}")
                                                    .ToList()
                })
                .ToList();

            return View(viewModel);
        }
    }
}
