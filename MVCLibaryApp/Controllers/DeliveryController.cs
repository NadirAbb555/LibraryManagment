using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;

namespace MVCLibaryApp.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: Delivery
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var action = db.TBLACTION.Where(m=>m.SD == false).ToList();
            return View(action);
        }
        public ActionResult History()
        {
            var action = db.TBLACTION.Where(m => m.SD == true).ToList();
            return View(action);
        }
        [HttpGet]
        public ActionResult DeliveryAdd()
        {
            List<SelectListItem> book = (from x in db.TBLBOOK.Where(z=>z.BOOKSTATUS == true && z.SD == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.NAME,
                                             Value = x.ID.ToString()
                                         }).ToList();
            ViewBag.book = book;

            List<SelectListItem> member = (from m in db.TBLMEMBERS.Where(z=>z.SD == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = m.NAME + " " + m.SURNAME + "{"+ m.MAIL + "}",
                                               Value = m.ID.ToString()
                                           }).ToList();
            ViewBag.member = member;

            List<SelectListItem> personal = (from p in db.TBLPERSONAL.Where(z => z.SD == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = p.PERSONAL,
                                                 Value = p.ID.ToString()
                                             }).ToList();
            ViewBag.personal = personal;

            return View();
        }
        [HttpPost]
        public ActionResult DeliveryAdd(TBLACTION a)
        {
            var bk = db.TBLBOOK.Where(x => x.ID == a.TBLBOOK.ID).FirstOrDefault();
            var mb = db.TBLMEMBERS.Where(x => x.ID == a.TBLMEMBERS.ID).FirstOrDefault();
            var ps = db.TBLPERSONAL.Where(x => x.ID == a.TBLPERSONAL.ID).FirstOrDefault();

            
            if ( bk.BOOKSTATUS == false)
            {
                bk.BOOKSTATUS = false;
                return RedirectPermanent("/Book/Index/");

            }
            a.TBLBOOK = bk;
            a.TBLMEMBERS = mb;
            a.TBLPERSONAL = ps;
            a.SD = false;           
            db.TBLACTION.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index"); 
            //var book = db.TBLBOOK.Where(m => m.ID == a.BOOK).FirstOrDefault();
            //var member = db.TBLMEMBERS.Where(m => m.ID == a.MEMBER).FirstOrDefault();
            //var bId = db.TBLBOOK.Find(book.ID);
            //int stock = Convert.ToInt32(bId.STOCK);
            //int resultStck = stock = stock - 1;
            //if (stock == 0 || stock < 0 || bId.BOOKSTATUS == false)
            //{
            //    bId.BOOKSTATUS = false;
            //    return RedirectPermanent("/Book/ChangeBook/" + bId.ID);

            //}
            //var reBook = db.TBLACTION.Where(m => m.MEMBER == member.ID && m.BOOK == book.ID).FirstOrDefault();
            //if (reBook.ID == book.ID && reBook.ID == member.ID) 
            //{
            //    Response.Write("................................................Such information is already available");
            //    return View();
            //}
            //if (stock == 0)
            //{
            //    bId.BOOKSTATUS = false;
            //}
            //bId.STOCK = Convert.ToInt32(resultStck);
        }
        public ActionResult Return(TBLACTION a)
        {
            var retBook = db.TBLACTION.Find(a.ID);
            DateTime d1 = DateTime.Parse(retBook.RETURNDATE.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan d3 = d2 - d1;
            ViewBag.fine = d3.TotalDays;
            return View("Return", retBook);
        }
        public ActionResult UpdateRetToDelivery(TBLACTION a)
        {
            var retBook = db.TBLACTION.Find(a.ID);
            retBook.RETURNNOW = a.RETURNNOW;          
            retBook.SD = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}