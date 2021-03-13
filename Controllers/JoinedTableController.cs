using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMSCodeFirst.Models;

namespace TMSCodeFirst.Controllers
{
    public class JoinedTableController : Controller
    {
        // GET: JoinedTable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContractorRequests()
        {
            TMSContext tmsc = new TMSContext();
            //string userid = @Session["SessionTenderId"].ToString();

            List<Contractor> contr = tmsc.Contractors.ToList();
            List<Tender> tender = tmsc.Tenders.ToList();
            List<TenderApplication> tenderApplied = tmsc.TenderApplications.ToList();

            var joinedTenderApplicationDetails = from u in contr
                                         join b in tenderApplied on u.UserId equals b.CurrentUserId into table1
                                         from b in table1
                                         join v in tender on b.CurrentTenderId equals v.TenderId into table2
                                         from v in table2
                                         where (b.IsApproved == "Approved" || b.IsApproved == "Pending" || b.IsApproved == "Rejected")
                                         select new JoinedTable { JoinContractor = u, JoinTender = v, JoinTenderApplication = b };
            return View(joinedTenderApplicationDetails);
        }
    }
}