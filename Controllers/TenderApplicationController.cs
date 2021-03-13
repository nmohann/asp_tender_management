using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMSCodeFirst.Models;

namespace TMSCodeFirst.Controllers
{
    public class TenderApplicationController : Controller
    {
        // GET: TenderApplication
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProfileImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProfileImage(TenderApplication tenderApplication)
        {
            tenderApplication.CurrentUserId = @Session["SessionUserId"].ToString();
            tenderApplication.CurrentTenderId = Session["SessionTenderId"].ToString();
                //@Session["SessionTenderId"].ToString();
            tenderApplication.IsEvaluated = "Not Evaluated";
            tenderApplication.IsApproved = "Pending";
            

            string fileName = Path.GetFileNameWithoutExtension(tenderApplication.DocImageFile.FileName);
            string extension = Path.GetExtension(tenderApplication.DocImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            tenderApplication.ImagePath = "~/Documents/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Documents/"), fileName);
            tenderApplication.DocImageFile.SaveAs(fileName);
            using (TMSContext db = new TMSContext())
            {
                db.TenderApplications.Add(tenderApplication);
                db.SaveChanges();
            }
            ModelState.Clear();
            return View();

        }
    }
}