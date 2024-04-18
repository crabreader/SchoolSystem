using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolSystem.Controllers
{
    public class StudentTeacherCourseController : Controller
    {
        private readonly SchoolSystemContext _context;

        public StudentTeacherCourseController(SchoolSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var teacherCourses = _context.Course.Select(c => new StudentTeacherCourseViewModel
            {
                Course = c,
                Teachers = c.Teachers,
                Students = c.Classes.SelectMany(cl => cl.Students)
            });

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower(); // Convert search string to lower case

                teacherCourses = teacherCourses.Where(c => c.Course.CourseName.ToLower().Contains(searchString));
            }

            return View(await teacherCourses.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await GetViewModelByCourseId(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            var allTeachers = await _context.Teacher.ToListAsync(); // Retrieve all teachers
            ViewBag.AllTeachers = allTeachers;

            // Render default edit view
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentTeacherCourseViewModel viewModel)
        {
            var courseId = viewModel.Course.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the course from the database including its associated teachers
                    var course = await _context.Course.Include(c => c.Teachers).FirstOrDefaultAsync(c => c.Id == courseId);

                    if (course == null)
                    {
                        return NotFound();
                    }

                    // Update other properties of the course
                    course.CourseName = viewModel.Course.CourseName; // Update course name, for example

                    // Update the course's teachers based on the selected checkboxes
                    course.Teachers.Clear(); // Remove all existing associations first

                    if (viewModel.SelectedTeachers != null)
                    {
                        foreach (var teacherId in viewModel.SelectedTeachers)
                        {
                            var teacher = await _context.Teacher.FindAsync(teacherId);
                            if (teacher != null)
                            {
                                course.Teachers.Add(teacher);
                            }
                        }
                    }

                    // Update the course in the database
                    _context.Update(course);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(courseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If ModelState is not valid, return the view with the current viewModel
            return View(viewModel);
        }
        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        public async Task<StudentTeacherCourseViewModel> GetViewModelByCourseId(int? courseId)
        {
            if (courseId == null)
            {
                return null;
            }

            var course = await _context.Course
                                        .Include(c => c.Teachers)
                                        .Include(c => c.Classes)
                                            .ThenInclude(cls => cls.Students)
                                        .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
            {
                return null;
            }

            var viewModel = new StudentTeacherCourseViewModel
            {
                Course = course,
                Teachers = course.Teachers,
                Students = course.Classes.SelectMany(cls => cls.Students)
            };

            return viewModel;
        }
    }
}
