using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TenderManagementSystem.Models;

namespace TenderManagementSystem.Controllers
{
    public class ContractorsController : Controller
    {
        private TenderManagementDBEntities3 db = new TenderManagementDBEntities3();

        // GET: Contractors
        //----------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------start--------------------------------------------------------------
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        //public ActionResult Logins()
        //{
        //    return View();
        //}
        [HttpPost]
        public ActionResult Login(Contractor r)
        {
            var g = db.Contractors.Where(a => a.UserId.Equals(r.UserId) &&
                                                           a.Password.Equals(r.Password)).FirstOrDefault();
            if (g != null)
            {
                return RedirectToAction("Home", "Contractors");
            }
            else
            {
                return RedirectToAction("Login", "Contractors");
            }
        }


        //------------------------------------------------------end---------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------

        public ActionResult Index()
        {
            return View(db.Contractors.ToList());
        }

        // GET: Contractors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        //-------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------start--------------------------------------------------------------
        // GET: Contractors/Create
        public ActionResult Create()
        {
            Contractor c = new Contractor();
            var lastuserid = db.Contractors.OrderByDescending(m => m.UserId).FirstOrDefault();
            if (lastuserid == null)
            {
                c.UserId = "TMSC001";
            }
            else
            {
                //using string substring method to get the number of the last inserted employee's EmployeeID
                c.UserId = "TMSC" + (Convert.ToInt32(lastuserid.UserId.Substring(4, lastuserid.UserId.Length - 4)) + 1).ToString("D3");
            }
            return View(c);   

        }
        //------------------------------------------------------end--------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FirstName,LastName,DOB,Gender,Email,Contact,Category,SecretQuestion,SecretAnswer,Password")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Contractors.Add(contractor);
                db.SaveChanges();
                return RedirectToAction("Success");
            }

            return View(contractor);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FirstName,LastName,DOB,Gender,Email,Contact,Category,SecretQuestion,SecretAnswer,Password")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
