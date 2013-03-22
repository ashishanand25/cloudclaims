using CloudClaims.Admin.ViewModels;
using CloudClaims.Model;
using CloudClaims.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims.Admin.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password) {
            return RedirectToAction("AdminDashboard", "Home");
        }

        [HttpGet]
        public ActionResult AdminDashboard() {
            
            return View(new vmDashboard());
        }

        [HttpGet]
        public ActionResult AddClient() {
            return View();
        }
        [HttpPost]
        public ActionResult AddClient(CCClient c) {
            if (!ModelState.IsValid) { return View(); }
            EverythingToDoWith_CCClient.createClient(c);
            return RedirectToAction("AdminDashboard");
        }
        [HttpGet]
        public ActionResult ViewClients() {
            return View(EverythingToDoWith_CCClient.getAllClients());
        }

        [HttpGet]
        public ActionResult ManageClient(int id) {
            return View(EverythingToDoWith_CCClient.getClientANDContactCardByID(id));
        }

        [HttpPost]
        public ActionResult UpdateClient(CCClientANDCCGenericContactCard c) {
            if (!ModelState.IsValid) { return View("ManageClient", c); }
            EverythingToDoWith_CCClient.updateClient(c);
            return RedirectToAction("ViewClients");
        }
        [HttpGet]
        public ActionResult BusinessTypes() {
            return View();
        }
    }
}
