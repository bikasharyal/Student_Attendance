using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Course
    {
        [Key]
        public int course_id { get; set; }

        [Required]
        [DisplayName("Course Name")]
        //[Index("unique_course_name", IsUnique = true)]
        public string course_name { get; set; }

        [Required]
        [DisplayName("Number of Semester")]
        public string no_of_semester { get; set; }
    }
}