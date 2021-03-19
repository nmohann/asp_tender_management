using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMSCodeFirst.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Display(Name ="Liked About")]
        public string Like { get; set; }

        [Display(Name ="Improvements Needed")]
        public string Dislike { get; set; }
    }
}