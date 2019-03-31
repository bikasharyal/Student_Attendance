using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Teacher
    {
        [Key]
        public int teacher_id { get; set; }

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
        public int user_id { get; set; }

        
        [ForeignKey("user_id")]
        public virtual User user_id_fk { get; set; }

      
    }
}