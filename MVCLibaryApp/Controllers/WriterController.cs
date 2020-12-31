using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
//using PagedList;
//using PagedList.Mvc;

namespace MVCLibaryApp.Controllers
{
    public class WriterController : Controller
    {
        // GET: Writer
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var writer = db.TBLWRITER.Where(m => m.SD == true).ToList();
            return View(writer);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(TBLWRITER w)
        {
            if (!ModelState.IsValid)
            {
                return View("AddWriter");
            }
            db.TBLWRITER.Add(w);
            w.SD = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeWriter(int id)
        {
            var writer = db.TBLWRITER.Find(id);
            return View("ChangeWriter", writer);
        }
        public ActionResult UpdateWriter(TBLWRITER w)
        {
            var writer = db.TBLWRITER.Find(w.ID);
            writer.NAME = w.NAME;
            writer.SURNAME = w.SURNAME;
            writer.ABOUT = w.ABOUT;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteWriter(int id)
        {
            var writer = db.TBLWRITER.Find(id);
            writer.SD = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}