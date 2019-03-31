using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Student
    {
        [Key]
        public int student_id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [Required]
        [DisplayName("Email")]
        public string email { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string phone_no { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public DateTime dob { get; set; }

        [Required]
        [DisplayName("Enrolled Date")]
        public DateTime enrolled_date { get; set; }

        [Required]
        public int user_id { get; set; }

        [Required]
        public int course_id { get; set; }

        [Required]
        public int group_id { get; set; }


        [ForeignKey("user_id")]
        public virtual User user_id_fk { get; set; }

        [ForeignKey("course_id")]
        public virtual Course course_id_fk { get; set; }

        [ForeignKey("group_id")]
        public virtual Group group_id_fk { get; set; }

    }
}