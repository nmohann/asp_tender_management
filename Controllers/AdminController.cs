using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenderManagementSystem.Models;

namespace TenderManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        TenderManagementDBEntities1 db = new TenderManagementDBEntities1();

        public ActionResult Home()
        {
            return View();
        }
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logins()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Logins(Admin r)
        {
            var g = db.Admins.Where(a => a.UserId.Equals(r.UserId) &&
                                              a.Password.Equals(r.Password)).FirstOrDefault();
            if (g != null)
            {
                return RedirectToAction("Home", "Admin");
            }
            else
            {
                return RedirectToAction("Logins", "Admin");

            }
        }
    }
}