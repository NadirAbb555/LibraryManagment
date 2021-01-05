using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;

namespace MVCLibaryApp.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Index()
        {
            var member = db.TBLMEMBERS.Where(m => m.SD == true).ToList();
            return View(member);
        }
        [HttpGet]
        public ActionResult AddMember()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMember(TBLMEMBERS m)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPersonal");
            }
            m.SD = true;
            db.TBLMEMBERS.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMember(int id)
        {
            var member = db.TBLMEMBERS.Find(id);
            member.SD = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChangeMember(int id)
        {
            var member = db.TBLMEMBERS.Find(id);
            return View("ChangeMember", member);
        }
        public ActionResult UpdateMember(TBLMEMBERS m)
        {
            var member = db.TBLMEMBERS.Find(m.ID);
            member.NAME = m.NAME;
            member.SURNAME = m.SURNAME;
            member.MAIL = m.MAIL;
            member.MOB = m.MOB;
            member.USERNAME = m.USERNAME;
            member.PASSWORD = m.PASSWORD;
            member.FOTO = m.FOTO;
            member.SCHOOL = m.SCHOOL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MemberHistory(int id)
        {
            var member = db.TBLMEMBERS.Where(x => x.ID == id).FirstOrDefault();
            ViewBag.member = member.NAME + " " + member.SURNAME;
            var memBookhis = db.TBLACTION.Where(x => x.MEMBER == id).ToList();
            return View(memBookhis);
        }

    }
}