using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TMSCodeFirst.Controllers
{
    public class TMSHomeController : Controller
    {
        // GET: TMSHome
        public ActionResult Index()
        {
            return View();
        }
    }
}