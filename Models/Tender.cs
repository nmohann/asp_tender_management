using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TMSCodeFirst.Models
{
    public class Tender
    {
        [Key]
        [Required]
        [Display(Name = "Tender Id")]
        public string TenderId { get; set; }

        [Required]
        [Display(Name = "Tender Name")]
        public string TenderName { get; set; }

        [Required]
        [Display(Name = "Tender Type")]
        public string TenderType { get; set; }

        [DataType(DataType.Date)/*, DisplayFormat(DataFormatString = "{?????0:MM/dd/yyyy}?????")*/]
        [Required]
        [Display(Name = "Tender Start Date")]
        public DateTime TenderStartDate { get; set; }

        [DataType(DataType.Date)/*, DisplayFormat(DataFormatString = "{?????0:MM/dd/yyyy}?????")*/]
        [Required]
        [Display(Name = "Tender End Date")]
        public DateTime TenderEndDate { get; set; }
    }
}