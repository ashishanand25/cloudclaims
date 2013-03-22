using CloudClaims.Data;
using CloudClaims.Data.Portal;
using CloudClaims.Model;
using CloudClaims.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public ActionResult AddInternal() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AddInternal(CCPersonANDCCGenericContactCard p) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(p);
                }
                p.person.clientID = ((CCPerson)Session["loggedInUser"]).clientID;
                p.person.personParentID = p.person.clientID;
                p.person.personParentTable = "CCClients";
                EverythingToDoWith_CCPersons.createInternalPerson(p);
                return RedirectToAction("ManageInternal");
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult ManageInternal() {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCPersons.getAllInternalPersons(((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult EditInternal(Guid id) {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCPersons.getInternalPersonANDContactCardByID(id, ((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult EditExternal(Guid id) {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCPersons.getExternalPersonANDContactCardByID(id, ((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditInternal(CCPersonANDCCGenericContactCard p) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(p);
                }
                try {
                    EverythingToDoWith_CCPersons.updateInternalPerson(p);
                    ViewData["message"] = "User updated successfully";
                    ViewData["type"] = CustomEnums.NotificationType.SUCCESS;
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                }
                return View(p);
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditExternal(CCPersonANDCCGenericContactCard p) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(p);
                }
                try {
                    EverythingToDoWith_CCPersons.updateExternalPerson(p);
                    ViewData["message"] = "User updated successfully";
                    ViewData["type"] = CustomEnums.NotificationType.SUCCESS;
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                }
                return View(p);
            } else return RedirectToAction("Index", "Login");
        }


        [HttpGet]
        public ActionResult AddExternal() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }
        
        [HttpPost]
        public ActionResult AddExternal(CCPersonANDCCGenericContactCard p) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(p);
                }
                p.person.clientID = ((CCPerson)Session["loggedInUser"]).clientID;
                p.person.personParentID = p.person.clientID;
                p.person.personParentTable = "CCClientOrganizations";
                int tempPersionID = EverythingToDoWith_CCPersons.createExternalPerson(p);
                return RedirectToAction("ManageExternal");
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult ManageExternal() {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCPersons.getAllExternalPersonsANDClientOrganizations(((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

    }
}
