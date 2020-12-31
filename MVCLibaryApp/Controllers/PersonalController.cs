using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;

namespace MVCLibaryApp.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var personal = db.TBLPERSONAL.Where(m => m.SD == true).ToList();
            return View(personal);
        }
        [HttpGet]
        public ActionResult AddPersonal()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPersonal(TBLPERSONAL p)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPersonal");
            }
            p.SD = true;
            db.TBLPERSONAL.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeletePersonal(int id)
        {
            var personal = db.TBLPERSONAL.Find(id);
            personal.SD = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChangePersonal(int id)
        {
            var personal = db.TBLPERSONAL.Find(id);
            return View("ChangePersonal", personal);
        }
        public ActionResult UpdatePersonal(TBLPERSONAL p)
        {
            if (!ModelState.IsValid)
            {
                return RedirectPermanent("/Personal/ChangePersonal/"+p.ID);
            }
            var personal = db.TBLPERSONAL.Find(p.ID);
            personal.PERSONAL = p.PERSONAL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}