using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMSCodeFirst.Models;
using System.Data.Entity;

namespace TMSCodeFirst.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateFeedback()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFeedback(Feedback feedback)
        {
            using (TMSContext db = new TMSContext())
            {
                if (ModelState.IsValid)
                {
                    db.Feedbacks.Add(feedback);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(feedback);
                }
            }
        }      
    }
}