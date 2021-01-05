using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
namespace MVCLibaryApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        DBKITABXANAEntities db = new DBKITABXANAEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(TBLMEMBERS m)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 5000);
            string uname = x.ToString();
            bool controller = false;
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            var mail = (from i in db.TBLMEMBERS.ToList()
                        where i.MAIL == m.MAIL
                        select i.MAIL).FirstOrDefault();
            for (int i = 0; i > 1; i++)
            {

            }

            if (m.MAIL != mail)
            {
                m.USERNAME = m.NAME + uname;
                controller = false;
                m.CONFIRMATION = false;
                m.SD = true;
                db.TBLMEMBERS.Add(m);
                db.SaveChanges();
                BuildEmailTemplate(m.ID);
                ViewBag.Message = "Uğurla qeydiyyatdan keçdiniz. Zəhmət olmasa mail adresinizi tətiqləyin.";

                return View();
            }
            else
            {
                controller = true;
                ViewBag.mail = controller;
                ViewBag.msg = "Daxil etdiyiniz email artiq istifade olunub";
                return View();
            }

        }

     
        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public ActionResult RegisterConfirm(int regId)
        {
            TBLMEMBERS Data = db.TBLMEMBERS.Where(x => x.ID == regId).FirstOrDefault();
            Data.CONFIRMATION = true;
            db.SaveChanges();
            var msg = "Sizin mail adresiniz testiqlendi";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/") + "Text" + ".cshtml");
            var regInfo = db.TBLMEMBERS.Where(x => x.ID == regID).FirstOrDefault();
            var url = "https://localhost:44377/" + "Register/Confirm?regId=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Mail adresinizi testiqleyin", body, regInfo.MAIL);
        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, cc, bcc, subject, body;
            from = "verifieyemail0@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendMail(mail);
        }

        public static void SendMail(MailMessage mail)
        {

            using (SmtpClient client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Timeout = 5000;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("verifieyemail0@gmail.com", "M@$ter555");
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}