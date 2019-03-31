using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Schedule
    {
        [Key]
        public int schedule_id { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public DateTime start_time { get; set; }

        [Required]
        [DisplayName("End Time")]
        public DateTime end_time { get; set; }

        [Required]
        [DisplayName("Day")]
        public string day { get; set; }

        [Required]
        [DisplayName("Room")]
        public string room { get; set; }

        [Required]
        [DisplayName("Class Type")]
        public string class_type{ get; set; }

        [Required]
        public int group_id { get; set; }

        [Required]
        public int module_id { get; set; }

        [ForeignKey("group_id")]
        public virtual Group course_id_fk { get; set; }

        [ForeignKey("module_id")]
        public virtual Module module_id_fk { get; set; }
    }
}