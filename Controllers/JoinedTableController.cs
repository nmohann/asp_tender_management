using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMSCodeFirst.Models;

namespace TMSCodeFirst.Controllers
{
    [Authorize]
    public class JoinedTableController : Controller
    {
        // GET: JoinedTable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestsEvaluation()
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
                                         where (b.IsEvaluated == "Pending")
                                         select new JoinedTable { JoinContractor = u, JoinTender = v, JoinTenderApplication = b };
            return View(joinedTenderApplicationDetails);
        }

        public ActionResult EvaluateRequest(int id)
        {
            using (TMSContext db = new TMSContext())
            {
                var p = db.TenderApplications.Single(m => m.Id == id);
                p.IsEvaluated = "Evaluated";
                db.SaveChanges();

                return RedirectToAction("RequestsEvaluation");
            }
        }

        public ActionResult RemoveRequest(int id)
        {
            using (TMSContext db = new TMSContext())
            {
                var p = db.TenderApplications.Single(m => m.Id == id);
                p.IsEvaluated = "Removed";
                db.SaveChanges();

                return RedirectToAction("RequestsEvaluation");
            }
        }


        public ActionResult RequestsApproval()
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
                                                 where (b.IsEvaluated == "Evaluated") && (b.IsApproved == "Pending")
                                                 select new JoinedTable { JoinContractor = u, JoinTender = v, JoinTenderApplication = b };
            return View(joinedTenderApplicationDetails);
        }

        public ActionResult ApproveRequest(int id)
        {
            using (TMSContext db = new TMSContext())
            {
                var p = db.TenderApplications.Single(m => m.Id == id);
                p.IsApproved = "Approved";
                db.SaveChanges();

                return RedirectToAction("RequestsApproval");
            }
        }

        public ActionResult RejectRequest(int id)
        {
            using (TMSContext db = new TMSContext())
            {
                var p = db.TenderApplications.Single(m => m.Id == id);
                p.IsApproved = "Rejected";
                db.SaveChanges();

                return RedirectToAction("RequestsApproval");
            }
        }

        public ActionResult ContractorHistory()
        {
            TMSContext tmsc = new TMSContext();
            string userid = @Session["SessionUserId"].ToString();

            List<Contractor> contr = tmsc.Contractors.ToList();
            List<Tender> tender = tmsc.Tenders.ToList();
            List<TenderApplication> tenderApplied = tmsc.TenderApplications.ToList();

            var joinedTenderApplicationDetails = from u in contr
                                                 join b in tenderApplied on u.UserId equals b.CurrentUserId into table1
                                                 from b in table1
                                                 join v in tender on b.CurrentTenderId equals v.TenderId into table2
                                                 from v in table2
                                                 where ((b.IsApproved == "Approved" || b.IsApproved == "Pending" || b.IsApproved == "Rejected") && b.CurrentUserId == userid )
                                                 select new JoinedTable { JoinContractor = u, JoinTender = v, JoinTenderApplication = b };
            return View(joinedTenderApplicationDetails);
        }
    }
}