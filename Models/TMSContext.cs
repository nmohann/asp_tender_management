//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data.Entity;

//namespace TMSCodeFirst.Models
//{
//    public class TMSContext : DbContext
//    {
//        public TMSContext():base("name=final")
//        {

//        }

//        public virtual DbSet<Contractor> Contractors { get; set; }

//        public virtual DbSet<Admin> Admins { get; set; }

//        public virtual DbSet<Tender> Tenders { get; set; }

//        public virtual DbSet<TenderApplication> TenderApplications { get; set; }
//        //public virtual DbSet<MinimumAgeAttribute> MinimumAgeAttributes { get; set; }

//        public virtual DbSet<Feedback>Feedbacks { get; set; }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TMSCodeFirst.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class TMSContext : IdentityDbContext<ApplicationUser>
    {
        public TMSContext() : base("name=final", throwIfV1Schema: false)
        {

        }

        public virtual DbSet<Contractor> Contractors { get; set; }

        public virtual DbSet<Admin> Admins { get; set; }

        public virtual DbSet<Tender> Tenders { get; set; }

        public virtual DbSet<TenderApplication> TenderApplications { get; set; }
        //public virtual DbSet<MinimumAgeAttribute> MinimumAgeAttributes { get; set; }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        public static TMSContext Create()
        {
            return new TMSContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}