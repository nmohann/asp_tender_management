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
        public string TenderId { get; set; }

        [Required]
        public string TenderName { get; set; }

        [Required]
        public string TenderType { get; set; }

        [DataType(DataType.Date)/*, DisplayFormat(DataFormatString = "{?????0:MM/dd/yyyy}?????")*/]
        [Required]
        public DateTime TenderStartDate { get; set; }

        [DataType(DataType.Date)/*, DisplayFormat(DataFormatString = "{?????0:MM/dd/yyyy}?????")*/]
        [Required]
        public DateTime TenderEndDate { get; set; }
    }
}