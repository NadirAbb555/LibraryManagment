using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibaryApp.Models.Entity;
using System.Web.Security;
namespace MVCLibaryApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DBKITABXANAEntities db = new DBKITABXANAEntities();

        string sess;
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(TBLMEMBERS m)
        {
            var login = db.TBLMEMBERS.FirstOrDefault(x => x.MAIL == m.MAIL && x.PASSWORD == m.PASSWORD);
            if (login != null)
            {
                FormsAuthentication.SetAuthCookie(login.MAIL, false);
                Session["mail"] = login.MAIL.ToString();
                sess = login.MAIL.ToString();
                //TempData["id"] = login.ID.ToString();
                //TempData["ad"] = login.NAME.ToString();
                //TempData["soyad"] = login.SURNAME.ToString();
                //TempData["sifre"] = login.PASSWORD.ToString();
                //TempData["mobil"] = login.MOB.ToString();
                //TempData["tehsil"] = login.SCHOOL.ToString();
                //TempData["sekil"] = login.FOTO.ToString()/*;*/

                return RedirectToAction("Inbox", "Message");
            }
            else
            {

                return View();
            }
        }
        public ActionResult Exit()
        {

            Response.Cookies.Clear();

            FormsAuthentication.SignOut();


            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "login");
        }

    }
}