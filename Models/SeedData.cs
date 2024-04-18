using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Data;
using SchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolSystem.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolSystemContext(
                serviceProvider.GetRequiredService<DbContextOptions<SchoolSystemContext>>()))
            {
                if (context.Class.Any() || context.Course.Any() || context.Teacher.Any() || context.Student.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed the database

                // Create sample classes
                var classes = new List<Class>
                {
                    new Class { ClassName = "1A" },
                    new Class { ClassName = "1B" }
                };
                context.Class.AddRange(classes);
                context.SaveChanges();

                // Create sample courses
                var courses = new List<Course>
                {
                    new Course { CourseName = "Programmering 1" },
                    new Course { CourseName = "Programmering 2" },
                    new Course { CourseName = "Matematik 1" }
                };
                context.Course.AddRange(courses);
                context.SaveChanges();

                // Create sample teachers
                var teachers = new List<Teacher>
                {
                    new Teacher { FirstName = "John", LastName = "Doe" },
                    new Teacher { FirstName = "Jane", LastName = "Smith" },
                    new Teacher { FirstName = "Jill", LastName = "Dwight" },
                    new Teacher { FirstName = "Reidar", LastName = "" },
                    new Teacher { FirstName = "Tobias", LastName = "" }
                };
                context.Teacher.AddRange(teachers);
                context.SaveChanges();

                // Create sample students
                var students = new List<Student>
                {
                    new Student { FirstName = "Alice", LastName = "Johnson", ClassId = classes[0].Id },
                    new Student { FirstName = "Emily", LastName = "Brown", ClassId = classes[0].Id },
                    new Student { FirstName = "Sarah", LastName = "Williams", ClassId = classes[0].Id },
                    new Student { FirstName = "David", LastName = "Brown", ClassId = classes[0].Id },
                    new Student { FirstName = "Emma", LastName = "Jones", ClassId = classes[1].Id },
                    new Student { FirstName = "Daniel", LastName = "Garcia", ClassId = classes[1].Id },
                    new Student { FirstName = "Ethan", LastName = "Anderson", ClassId = classes[1].Id }
                };
                context.Student.AddRange(students);
                context.SaveChanges();

                // Add students to classes
                classes[0].Students = new List<Student> { students[0], students[1], students[2], students[3] };
                classes[1].Students = new List<Student> { students[4], students[5], students[6] };
                context.SaveChanges();

                // Add courses to classes
                classes[0].Courses = new List<Course> { courses[0] };
                classes[1].Courses = new List<Course> { courses[1], courses[2] };
                context.SaveChanges();

                // Add courses to teachers
                teachers[0].Courses = new List<Course> { courses[0] };
                teachers[1].Courses = new List<Course> { courses[1] };
                context.SaveChanges();

                // Add classes to courses
                courses[0].Classes = new List<Class> { classes[0] };
                courses[1].Classes = new List<Class> { classes[1] };
                context.SaveChanges();

                // Add teachers to courses
                courses[0].Teachers = new List<Teacher> { teachers[0], teachers[1], teachers[2] };
                courses[1].Teachers = new List<Teacher> { teachers[1] };
                courses[2].Teachers = new List<Teacher> { teachers[2] };
                context.SaveChanges();
            }
        }
    }
}
