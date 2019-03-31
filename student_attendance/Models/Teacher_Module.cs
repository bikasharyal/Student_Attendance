using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Teacher_Module
    {
        [Key]
        public int id { get; set; }
              
        [Required]
        public int teacher_id { get; set; }

        
        [Required]
        public int module_id { get; set; }

        [ForeignKey("teacher_id")]
        public virtual Teacher teacher_id_fk { get; set; }

        [ForeignKey("module_id")]
        public virtual Module module_id_fk { get; set; }

        [Required]
        [DisplayName("Teacher Type")]
        public string teacher_type { get; set; }

    }
}