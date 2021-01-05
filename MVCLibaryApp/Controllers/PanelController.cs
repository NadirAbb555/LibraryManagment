using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;

namespace MVCLibaryApp.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            string adsoyad = TempData["ad"].ToString() + " " + TempData["soyad"].ToString();
            ViewBag.AdSoyad = adsoyad;
            var member = (string)Session["mail"];
            var data = db.TBLMEMBERS.FirstOrDefault(x => x.MAIL == member);
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(TBLMEMBERS m)
        {
            var member = (string)Session["mail"];
            var find = db.TBLMEMBERS.FirstOrDefault(x => x.MAIL == member);
            find.PASSWORD = m.PASSWORD;
            find.FOTO = m.FOTO;
            find.SCHOOL = m.SCHOOL;
            find.USERNAME = "NON";
            db.SaveChanges();
          
            return RedirectToAction("Index", "Panel");
        }
        RegisterController rc = new RegisterController();
        public ActionResult ConfirmAccaunt(TBLMEMBERS m)
        {
            rc.BuildEmailTemplate(m.ID);
            return RedirectToAction("Login","Login");
        }
        public ActionResult Books()
        {
            var member = (string)Session["mail"];
            int id = db.TBLMEMBERS.Where(x => x.MAIL == member.ToString()).Select(z => z.ID).FirstOrDefault();
            var action = db.TBLACTION.Where(m => m.MEMBER == id).ToList();
            return View(action);
        }
        public ActionResult BaseLayout()
        {
            return View();
        }
        }
}