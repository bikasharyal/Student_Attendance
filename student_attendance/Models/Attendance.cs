using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Attendance
    {
        [Key]
        public int attendance_id { get; set; }
        
        [Required]
        [DisplayName("Date")]
        public DateTime date { get; set; }
                        
        [Required]
        [DisplayName("Entry Time")]
        public DateTime entry_time { get; set; }

        [Required]
        [DisplayName("Date")]
        public string status { get; set; }

        [Required]
        public int student_id { get; set; }

        [Required]
        public int schedule_id { get; set; }

        [ForeignKey("student_id")]
        public virtual Student student_id_fk { get; set; }

        [ForeignKey("schedule_id")]
        public virtual Schedule schedule_id_fk { get; set; }

        
    }
}