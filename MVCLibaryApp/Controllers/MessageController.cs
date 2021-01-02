using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;

namespace MVCLibaryApp.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        DBKITABXANAEntities db = new DBKITABXANAEntities();
        public ActionResult Inbox()
        {
            try
            {
                var inbox = (string)Session["mail"].ToString();
                var msg = (from m in db.TBLMESSAGE.Where(x => x.RECEIVER == inbox.ToString() && x.STRECEIVER == false).ToList()
                           orderby m.ID descending
                           select m).ToList();
                return View(msg);
            }
            catch (Exception)
            {
             
                return RedirectPermanent("~/Login/Login");

            }
        }
        public ActionResult Sent()
        {
            var inbox = (string)Session["mail"].ToString();
            var msg = db.TBLMESSAGE.Where(x => x.SENDER == inbox.ToString() && x.STSENDER == false).ToList();
            return View(msg);
        }

        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Send(TBLMESSAGE m)
        {
            var inbox = (string)Session["mail"].ToString();

            var dbMail = db.TBLMEMBERS.Where(x => x.MAIL == m.RECEIVER).FirstOrDefault();

            if (m.RECEIVER == dbMail.MAIL)
            {
                m.READMSG = false;                
                m.SENDER = inbox.ToString();
                m.DATE = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                db.TBLMESSAGE.Add(m);
                db.SaveChanges();
                return RedirectToAction("Sent", "Message");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Read(TBLMESSAGE m)
        {
            var readMsgId = db.TBLMESSAGE.Find(m.ID);
            readMsgId.READMSG = true;
            db.SaveChanges();
            return RedirectToAction("Inbox", "Message");
        }
        public ActionResult DeleteInbox(TBLMESSAGE m)
        {
            var delMSG = db.TBLMESSAGE.Find(m.ID);
            delMSG.STRECEIVER = true;
            db.SaveChanges();
            return RedirectToAction("Inbox", "Message");
        }

        public ActionResult DeleteSent(TBLMESSAGE m)
        {
            var delMSG = db.TBLMESSAGE.Find(m.ID);
            delMSG.STSENDER = true;
            db.SaveChanges();
            return RedirectToAction("Inbox", "Message");
        }

    }
}