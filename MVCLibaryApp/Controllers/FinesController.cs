using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
namespace MVCLibaryApp.Controllers
{
    public class FinesController : Controller
    {
        // GET: Fines
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var fines = (from f in db.TBLFINE.Where(x=>x.FINE > 0).ToList()
                         orderby f.ID descending
                         select f).ToList();
            return View(fines);
        }
    }
}