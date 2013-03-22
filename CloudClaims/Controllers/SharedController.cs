using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudClaims.ViewModels;
using CloudClaims.Data;
using CloudClaims.Data.Portal;
using CloudClaims.Model;

namespace CloudClaims.Controllers {
    public class SharedController : Controller {
        public ActionResult Notifications(vmNotifications notif) {
            return PartialView(notif);
        }

        public ActionResult PersonPicker(vmPersonPicker pp) {
            if (Session["loggedInUser"] != null) {
                try {
                    //vmPersonPicker pp = new vmPersonPicker();
                    //pp.personType = CustomEnums.PersonType.INTERNAL;
                    if (pp.personType == CustomEnums.PersonType.INTERNAL) {
                        pp.person = EverythingToDoWith_CCPersons.getAllInternalPersons(((CCPerson)Session["loggedInUser"]).clientID);
                    } else if (pp.personType == CustomEnums.PersonType.EXTERNAL) {
                        pp.person = EverythingToDoWith_CCPersons.getAllExternalPersons(((CCPerson)Session["loggedInUser"]).clientID);
                    }
                    return PartialView(pp);
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        //[HttpPost]
        //public JsonResult updateEntity(string objectType, int objectID, bool objectState) {
        //    if (Session["loggedInUser"] != null) {
        //        try {
        //            GenericDataAccess.updateEntity(objectType, objectID, objectState);
        //            return Json(new vmNotifications {
        //                type = Data.CustomEnums.NotificationType.SUCCESS,
        //                message = String.Format("Setting updated successfully")
        //            });
        //        } catch (Exception e) {
        //            return Json(new vmNotifications {
        //                type = Data.CustomEnums.NotificationType.FAILURE,
        //                message = e.Message
        //            });
        //        }
        //    } else return Json(new vmNotifications {
        //        type = Data.CustomEnums.NotificationType.FAILURE,
        //        message = "Must be logged in!"
        //    });
        //}
    }
}
