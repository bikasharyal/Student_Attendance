using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class AllRole
    {
        [Key]
        public int role_id { get; set; }
      
        [DisplayName("Role")]        
        public string role { get; set; }

    }
}