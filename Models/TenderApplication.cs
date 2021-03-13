using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMSCodeFirst.Models
{
    public class TenderApplication
    {
        [Key]
        public int Id { get; set; }
        public string CurrentTenderId { get; set; }
        public string CurrentUserId { get; set; }
        public double Quotation { get; set; }
        public string ImagePath { get; set; }
        public string IsEvaluated { get; set; }
        public string IsApproved { get; set; }

        [NotMapped]
        public HttpPostedFileBase DocImageFile { get; set; }
    }
}