using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace TMSCodeFirst.Models
{
    public class TechnicalFeedback
    {
            [Key]

            public int Id { get; set; }

            [Required]
            [Display(Name = "Your Contact:")]
            public string YourContact { get; set; }

            [Required]
            [Display(Name = "Your Email")]
            public string MaildId { get; set; }

            [Required]
            [Display(Name = "Your FirstName")]
            public string YourName { get; set; }

            [Required]
            [Display(Name = "Difficulty About:")]
            public string DifficultyYouAreFacing { get; set; }


            [Required]
            [Display(Name = "Details:")]
            public string Body { get; set; }


        }
}

