using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMSCodeFirst.Models
{
    public class JoinedTable
    {
        public Contractor JoinContractor { get; set; }

        public Tender JoinTender { get; set; }

        public TenderApplication JoinTenderApplication { get; set; }


    }
}