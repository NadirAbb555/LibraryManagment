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
    public class BookController : Controller
    {
        // GET: Book

        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public void SelectCategory()
        {
            List<SelectListItem> category = (from c in db.TBLCATEGORY.Where(m => m.SD == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = c.NAME,
                                                 Value = c.ID.ToString()
                                             }).ToList();
            ViewBag.cat = category;
        }
        public void SelectWriter()
        {
            List<SelectListItem> writer = (from x in db.TBLWRITER.Where(m => m.SD == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.NAME + " " + x.SURNAME,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.writer = writer;
        }
        public ActionResult Index()
        {
            var book = db.TBLBOOK.Where(m => m.SD == true).ToList();
            return View(book);
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            //category
            SelectCategory();
            //writer
            SelectWriter();
            return View();
        }
        [HttpPost]
        public ActionResult AddBook(TBLBOOK b)
        {
            var category = db.TBLCATEGORY.Where(m => m.ID == b.TBLCATEGORY.ID).FirstOrDefault();
            var writer = db.TBLWRITER.Where(m => m.ID == b.TBLWRITER.ID).FirstOrDefault();
            b.TBLCATEGORY = category;
            b.TBLWRITER = writer;
            b.SD = true;
            b.BOOKSTATUS = true;
            db.TBLBOOK.Add(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteBook(int id)
        {
            var book = db.TBLBOOK.Find(id);
            book.SD = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChangeBook(int id)
        {
            var book = db.TBLBOOK.Find(id);
            SelectCategory();
            SelectWriter();
            return View("ChangeBook", book);
        }
        public ActionResult UpdateBook(TBLBOOK b)
        {
            var book = db.TBLBOOK.Find(b.ID);
            book.NAME = b.NAME;
            book.PAGES = b.PAGES;
            book.PHOTO = b.PHOTO;
            book.DATE = b.DATE;
            book.PUBLISHING = b.PUBLISHING;
            var category = db.TBLCATEGORY.Where(m => m.ID == b.TBLCATEGORY.ID).FirstOrDefault();
            var writer = db.TBLWRITER.Where(m => m.ID == b.TBLWRITER.ID).FirstOrDefault();
            book.CATEGORY = category.ID;
            book.WRITER = writer.ID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult WritersBook(int id)
        {
            var writer = db.TBLWRITER.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.writer = writer.NAME + " " + writer.SURNAME;
            var writer_book = db.TBLBOOK.Where(x => x.WRITER == id).ToList();
            return View(writer_book);
        }
    }
}