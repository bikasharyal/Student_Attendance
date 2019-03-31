using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [DisplayName("Username")]
        [Index("unique_username_name", IsUnique = true)]
        public string user_name { get; set; }

        [Required]
        [DisplayName("Password")]
        public string password { get; set; }

        [Required]
        [DisplayName("Type")]
        public string type { get; set; }

    }
}