using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudClaims.ViewModels;
using CloudClaims.Model;
using CloudClaims.Data.Portal;
using System.Web.Security;

namespace CloudClaims.Controllers {
    public class LoginController : Controller {
        [HttpGet]
        public ActionResult Index() {
            CCPerson loggedInPlayer = new CCPerson();
            loggedInPlayer = (CCPerson)Session["loggedInUser"];
            if (loggedInPlayer == null) {
                loggedInPlayer = PortalUtils.readCookieData(User.Identity);
            }
            if (loggedInPlayer.personID > 0) {
                return RedirectToAction("Index", "Home");
            } else return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult DoLogin(SignIn s) {
            CCPerson loggedInUser = Authentication.DoClientLogin(s.username, s.password);
            if (loggedInUser != null) {
                Session["loggedInUser"] = loggedInUser;
                string cookieString = String.Format("{0}|{1}|{2}", loggedInUser.personGUID, loggedInUser.clientID, loggedInUser.personFName);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, loggedInUser.personGUID.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, cookieString, FormsAuthentication.FormsCookiePath);
                string hashCookies = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            } else {
                return RedirectToAction("Index", "Login");
            }
        }
        
        public ActionResult DoLogout() {
            Session["loggedInUser"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login", new { l = "1" });
        }
    }
}
