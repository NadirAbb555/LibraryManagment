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
    }
}