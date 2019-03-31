using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class DataContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher_Module> Teacher_Modules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}