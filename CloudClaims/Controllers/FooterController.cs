using CloudClaims.Data;
using CloudClaims.Data.Portal;
using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims.Controllers
{
    public class FooterController : Controller
    {
        public ActionResult TextboxPicker() {
            if (Session["loggedInUser"] != null) {
                try {
                        return PartialView(ETDW_CCSetting_ClaimTextboxTypes.getAllClaimTextboxTypes(((CCPerson)Session["loggedInUser"]).clientID));
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult DateFieldPicker() {
            if (Session["loggedInUser"] != null) {
                try {
                    return PartialView(EverythingToDoWith_CCClaimDateTypes.getAllClaimDateTypesByClientID(((CCPerson)Session["loggedInUser"]).clientID, true));
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult DropdownPicker() {
            if (Session["loggedInUser"] != null) {
                try {
                    return PartialView(EverythingToDoWith_CCSetting_ClaimDropdownTypes.getAllClaimDropdownTypes(((CCPerson)Session["loggedInUser"]).clientID));
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult CheckboxPicker() {
            if (Session["loggedInUser"] != null) {
                try {
                    return PartialView(ETDW_CCSetting_ClaimCheckboxTypes.getAllClaimCheckboxTypes(((CCPerson)Session["loggedInUser"]).clientID));
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult PersonnelTypePicker() {
            if (Session["loggedInUser"] != null) {
                try {
                    return PartialView(EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getAllClaimPersonnelTypes(((CCPerson)Session["loggedInUser"]).clientID, true));
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }
    }
}
