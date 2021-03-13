using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMSCodeFirst.Models
{
    public class Admin
    {
        [Key]
        [Required(ErrorMessage = "Please enter the UserId")]
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }

    }
}