using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TMSCodeFirst.Models
{
    public class TMSContext : DbContext
    {
        ////public TMSContext()
        ////{

        ////}

        public virtual DbSet<Contractor> Contractors { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Tender> Tenders { get; set; }

        public virtual DbSet<TenderApplication> TenderApplications { get; set; }
        //public virtual DbSet<MinimumAgeAttribute> MinimumAgeAttributes { get; set; }
    }
}