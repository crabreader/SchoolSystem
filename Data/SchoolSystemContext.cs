using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;

namespace SchoolSystem.Data
{
    public class SchoolSystemContext : DbContext
    {
        public SchoolSystemContext (DbContextOptions<SchoolSystemContext> options)
            : base(options)
        {
        }

        public DbSet<SchoolSystem.Models.Class> Class { get; set; } = default!;
        public DbSet<SchoolSystem.Models.Course> Course { get; set; } = default!;
        public DbSet<SchoolSystem.Models.Student> Student { get; set; } = default!;
        public DbSet<SchoolSystem.Models.Teacher> Teacher { get; set; } = default!;
    }
}
