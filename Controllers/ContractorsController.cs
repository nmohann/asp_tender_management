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
using Microsoft.AspNet.Identity.EntityFramework;

namespace TMSCodeFirst.Controllers
{
    [Authorize]
    public class ContractorsController : Controller
    {
        private TMSContext db = new TMSContext();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ContractorsController()
        {

        }

        public ContractorsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Contractors
        public ActionResult Index()
        {
            using (TMSContext db = new TMSContext())
            {
                return View(db.Tenders.ToList());
            }
        }

        public ActionResult displaycontractors()
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


        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.



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
        // GET: Contractors/Create
        [AllowAnonymous]
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,FirstName,LastName,DOB,Gender,Email,Contact,Category,SecretQuestion,SecretAnswer,Password")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Id = contractor.UserId.ToString(), UserName = contractor.FirstName.ToString(), Email = contractor.Email.ToString() };
                var result = await UserManager.CreateAsync(user, contractor.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    db.Contractors.Add(contractor);
                    db.SaveChanges();
                    return RedirectToAction("Success");

                }
                AddErrors(result);
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

        //--------------------------------------------------------------------------------

        public ActionResult Home()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Success()
        {
            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "TMSHome");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //public ActionResult Logins()
        //{
        //    return View();
        //}
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Contractor r)
        {
            var g = db.Contractors.Where(a => a.UserId.Equals(r.UserId)).FirstOrDefault();
            if (g == null)
            {
                ModelState.AddModelError("", String.Format("User ID {0} not present.", r.UserId));
                return View(r);
            }

            var gg = db.Contractors.Where(a => a.UserId.Equals(r.UserId) && a.Password.Equals(r.Password)).FirstOrDefault();
            if (gg != null)
            {
                var result = await SignInManager.PasswordSignInAsync(gg.FirstName, r.Password, false, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            {
                                Session["SessionUserId"] = r.UserId.ToString();
                                //Session["SessionFirstName"] = r.FirstName.ToString();
                                //Session["SessionLastName"] = r.LastName.ToString();
                                //Session["SessionContact"] = r.Contact.ToString();

                                return RedirectToAction("Index", "Contractors");
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

        [AllowAnonymous]
        public ActionResult ForgotUsername()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotUsername(Contractor c)
        {

            using (TMSContext db = new TMSContext())
            {
                try
                {
                    var usr = db.Contractors.Single(u => u.Contact == c.Contact && u.Email == c.Email && u.SecretQuestion == c.SecretQuestion && u.SecretAnswer == c.SecretAnswer);
                    if (c != null)
                    {
                        ViewBag.Message = usr.UserId.ToString();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Credentials! ❌");
                    }
                }
                catch
                {

                    ModelState.AddModelError("", "Invalid Credentials! ❌");
                }
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(Contractor c)
        {

            using (TMSContext db = new TMSContext())
            {
                try
                {
                    var usr = db.Contractors.Single(u => u.UserId == c.UserId && u.Contact == c.Contact && u.Email == c.Email);
                    if (c != null)
                    {
                        Session["SessionUserId"] = usr.UserId.ToString();


                        return RedirectToAction("ChangePassword");
                    }
                    else
                    {
                        ModelState.AddModelError("", "I am sure youre not registered with us!.");

                    }
                }

                catch
                {
                    ModelState.AddModelError("", "I am sure youre not registered with us!..");


                }
            }


            return View();
        }


        [AllowAnonymous]
        public ActionResult ChangePassword()
        {

            return View();

        }

        // POST: CensusTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ChangePassword(Contractor c)
        {
            using (TMSContext db = new TMSContext())
            {

                if (c.Password != null)
                {

                    string id = @Session["SessionUserId"].ToString();
                    var usr = db.Contractors.Where(u => u.UserId == id).First();
                    usr.Password = c.Password;

                    db.SaveChanges();
                    return RedirectToAction("Login");

                }


                else
                {
                    ModelState.AddModelError("", "Password Error!.");

                }
                //user.UserConfirmPassword =usr.UserConfirmPassword ;
                //db.Entry(user).State = EntityState.Modified;
                //db.SaveChanges();


                return View();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "TMSHome");
        }

        public ActionResult AvailableTenders()
        {
            //Session["SessionUserId"] = r.UserId.ToString();
            using (TMSContext db = new TMSContext())
            {
                return View(db.Tenders.ToList());
            }
        }

        public ActionResult AvailableTenderDetails(string Id)
        {

            //Session["AppliedTenderId"] = Id;
            using (TMSContext db = new TMSContext())
            {
                Session["SessionTenderId"] = Id.ToString();
                if (Id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tender tender = db.Tenders.Find(Id);
                if (tender == null)
                {
                    return HttpNotFound();
                }
                return View(tender);
            }
        }

        public ActionResult Help()
        {
            return View();
        }



        //-------------------------------------------------------------------------------------

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
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
