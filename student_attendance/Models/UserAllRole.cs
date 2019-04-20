using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace student_attendance.Models
{
    public class UserAllRole
    {
        [Key]
        public int user_role_id { get; set; }

        [Required]
        [DisplayName("User")]
        public int user_id { get; set; }

        [Required]
        [DisplayName("Role")]
        public int role_id { get; set; }

        [ForeignKey("user_id")]
        public virtual User user_id_fk { get; set; }

        [ForeignKey("role_id")]
        public virtual AllRole role_id_fk { get; set; }

    }
}