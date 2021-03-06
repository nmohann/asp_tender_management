using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMSCodeFirst.Models
{
    public class Contractor
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Please enter the UserId")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter First name")]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last name")]
        [StringLength(15, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Select Date of Birth")]
        [MinimumAge(18, ErrorMessage = "Minimum age for registration is 18.")]
        [DataType(DataType.Date)/*, DisplayFormat(DataFormatString = "{?????0:MM/dd/yyyy hh:mm:ss}?????")*/]
        public System.DateTime DOB { get; set; }

        [Required(ErrorMessage = "Please Select your Gender")]
        public string Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please input your Contact Number")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Please Select your category")]
        public string Category { get; set; }


        public string SecretQuestion { get; set; }

        [Required(ErrorMessage = "Please answer the question!")]
        public string SecretAnswer { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the password")]
        public string Password { get; set; }
    }
}