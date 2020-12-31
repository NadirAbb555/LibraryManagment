using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
using MVCLibaryApp.Models.ManyTables;

namespace MVCLibaryApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var cash = db.TBLFINE.Where(x => x.FINE > 0).Sum(m => m.FINE);
            var member = db.TBLMEMBERS.Count();
            var book = db.TBLBOOK.Count();
            var deliveryBook = db.TBLBOOK.Where(m => m.BOOKSTATUS == false).Count();
            ViewBag.cash = cash;
            ViewBag.member = member;
            ViewBag.book = book;
            ViewBag.deliveryBook = deliveryBook;

            // col //
            var personal = db.TBLPERSONAL.Count();
            ViewBag.per = personal;

            var writer = db.TBLWRITER.Count();
            ViewBag.wrt = writer;

            var message = db.TBLCONTACT.Count();
            ViewBag.msg = message;

            var category = db.TBLCATEGORY.Count();
            ViewBag.cat = category;

            var bestBookWrt = db.BestBookWriter().FirstOrDefault();
            ViewBag.WrtBook = bestBookWrt;

            var publishing = db.TBLBOOK.GroupBy(x => x.PUBLISHING).OrderByDescending(y => y.Count()).Select(z =>
            new
            {
                z.Key
            }).FirstOrDefault();
            ViewBag.pblsh = publishing;

            var bst_member = db.BestMemberAction().FirstOrDefault();
            ViewBag.bstMbr = bst_member;

            var bst_personal = db.BestPersonalAction().FirstOrDefault();
            ViewBag.bstPer = bst_personal;

            var delivery = db.TBLACTION.Count();
            ViewBag.dlvry = delivery;


            var dailyDelivry = db.TBLACTION.Where(x => x.DELIVERYDATE == DateTime.Today).Select(c => c.BOOK).Count();
            ViewBag.dlyDlvry = dailyDelivry;

            var returned = db.TBLACTION.Where(m => m.SD == true).Count();
            ViewBag.returned = returned;

            var readBook = db.BestBookAction().FirstOrDefault();
            ViewBag.readBk = readBook;

            return View();
        }
        public ActionResult WeatherWidget()
        {
            return View();
        }

        public ActionResult Galery()
        {
            return View();
        }
    }
}