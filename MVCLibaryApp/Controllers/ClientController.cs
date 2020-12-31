using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
using MVCLibaryApp.Models.ManyTables;

namespace MVCLibaryApp.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        
        [HttpGet]
        public ActionResult Index()
        {
            Tables tcs = new Tables();
            tcs.book = db.TBLBOOK.ToList();
            tcs.about = db.TBLSITEABOUT.ToList();
            return View(tcs);
        }

        [HttpPost]
        public ActionResult Index2(TBLCONTACT c)
        {
            db.TBLCONTACT.Add(c);
            db.SaveChanges();
            return RedirectPermanent("/Client/Index");
        }
    }
}