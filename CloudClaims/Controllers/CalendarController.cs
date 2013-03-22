using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims.Controllers
{
    public class CalendarController : Controller
    {
        //
        // GET: /Calendar/

        public ActionResult Index() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

    }
}
