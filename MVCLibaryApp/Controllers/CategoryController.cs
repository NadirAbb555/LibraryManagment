using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCLibaryApp.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        DBKITABXANAEntities db = new DBKITABXANAEntities();

        public ActionResult Index(string p, int page = 1)
        {
            //var category = db.TBLCATEGORY.Where(m=>m.SD == true).ToList().ToPagedList(page,4);

            var category = from c in db.TBLCATEGORY.Where(m => m.SD == true) select c;
            if (!string.IsNullOrEmpty(p))
            {
                category = category.Where(m => m.NAME.Contains(p));
            }
            return View(category.ToList().ToPagedList(page, 4));
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(TBLCATEGORY c)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCategory");

            }

            db.TBLCATEGORY.Add(c);
            c.SD = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ChangeCategory(int id)
        {
            var category = db.TBLCATEGORY.Find(id);
            return View("ChangeCategory", category);
        }

        public ActionResult UpdateCategory(TBLCATEGORY c)
        {
            var category = db.TBLCATEGORY.Find(c.ID);
            category.NAME = c.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.TBLCATEGORY.Find(id);
            category.SD = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}