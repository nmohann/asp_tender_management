using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using TMSCodeFirst.Models;


namespace TMSCodeFirst.Controllers
{
    [Authorize]
    public class TechnicalFeedbackController : Controller
    {
        public ActionResult TriggerMail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TriggerMail(TechnicalFeedback model, Contractor user)
        {

            MailMessage mm = new MailMessage("partyplanninguser@gmail.com", "vaibhavzode003@gmail.com");
            mm.Subject =Session["SessionUserId"].ToString() + " is facing " + model.DifficultyYouAreFacing+ " issue.";
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("partyplanninguser@gmail.com", "PPuser@123");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ModelState.Clear();
            ViewBag.Message = @Session["SessionUserId"].ToString() +
                " We have noted that and also sent an E-mail to admin about the issue! ";

            return View();
        }
    }
}



    
