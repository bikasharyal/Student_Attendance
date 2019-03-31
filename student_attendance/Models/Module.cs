using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Module
    {
        [Key]
        public int module_id { get; set; }

        [Required]
        [DisplayName("Module Code")]
        //[Index("unique_module_code", IsUnique = true)]
        public string module_code { get; set; }

        [Required]
        [DisplayName("Module Name")]
        //[Index("unique_module_name", IsUnique = true)]
        public string module_name { get; set; }

        [Required]
        [DisplayName("Credit Hour")]
        public string credit_hour { get; set; }

        [Required]
        [DisplayName("Semester")]
        public string semester { get; set; }

        [Required]
        public int course_id { get; set; }

        [ForeignKey("course_id")]
        public virtual Course course_id_fk { get; set; }
    }
}