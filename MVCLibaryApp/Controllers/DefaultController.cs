using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
namespace MVCLibaryApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var cat = db.TBLCATEGORY.ToList();
            return View(cat);
        }

        public ActionResult ViewCat(int id)
        {
            var category = db.TBLCATEGORY.Find(id);
            var books = db.TBLBOOK.Where(m => m.CATEGORY == category.ID).ToList();
            return View(books);
        }
    }
}