using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class Group
    {
        [Key]
        public int group_id { get; set; }

        [Required]
        [DisplayName("Group Name")]
        public string name { get; set; }
               
    }
}