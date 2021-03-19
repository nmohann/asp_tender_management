using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TMSCodeFirst.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace TMSCodeFirst.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        // GET: Admins

        private TMSContext db = new TMSContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminsController()
        {

        }

        public AdminsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: Admins
        public ActionResult Index()
        {
            using (TMSContext db = new TMSContext())
            {
                return View(db.Tenders.ToList());
            }
        }

        // GET: Admins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //----------------------------------------------------------------------------------

        public ActionResult Home()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Admin r)
        {
            var g = db.Admins.Where(a => a.UserId.Equals(r.UserId)).FirstOrDefault();
            if (g == null)
            {
                ModelState.AddModelError("", String.Format("User ID not present.", r.UserId));
                return View(r);
            }

            var gg = db.Admins.Where(a => a.UserId.Equals(r.UserId) && a.Password.Equals(r.Password)).FirstOrDefault();
            if (gg != null)
            {
                var result = await SignInManager.PasswordSignInAsync(gg.UserId, r.Password, false, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            {
                                Session["SessionUserId"] = r.UserId.ToString();
                                return RedirectToAction("Index", "Admins");
                            }
                        }
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Password not matching.");
                        return View(r);
                    default:
                        ModelState.AddModelError("", "Password not matching.");
                        return View(r);

                }
            }
            else
            {
                ModelState.AddModelError("", "Password not matching.");
                return View(r);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "TMSHome");
        }

        public ActionResult AddTender()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTender(Tender tender)
        {
            db.Tenders.Add(tender);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult AvailableFeedbacks()
        {

            using (TMSContext db = new TMSContext())
            {
                return View(db.Feedbacks.ToList());
            }
        }
        //-------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}