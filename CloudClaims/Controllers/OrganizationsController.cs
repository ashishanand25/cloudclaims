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
    public class OrganizationsController : Controller
    {
        [HttpGet]
        public ActionResult Add() {
            if (Session["loggedInUser"] != null) {
                return View(new CCClientOrganizationANDCCGenericContactCard());
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Add(CCClientOrganizationANDCCGenericContactCard o) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(o);
                }
                o.org.clientID = ((CCPerson)Session["loggedInUser"]).clientID;
                EverythingToDoWith_CCClientOrganizations.createOrganization(o);
                return RedirectToAction("Manage");
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult Manage() {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCClientOrganizations.getAllOrganizationsAndContactCardsFor((((CCPerson)Session["loggedInUser"]).clientID)));
            } else return RedirectToAction("Index", "Login");
        }

        
        public ActionResult ClientOrganizations(int selectedID) {
            if (Session["loggedInUser"] != null) {
                List<CCClientOrganizationDropDown> ccodd = new List<CCClientOrganizationDropDown>();
                foreach (CCClientOrganization o in EverythingToDoWith_CCClientOrganizations.getAllOrganizations((((CCPerson)Session["loggedInUser"]).clientID))) {
                    if (o.clientOrgID == selectedID) {
                        ccodd.Add(new CCClientOrganizationDropDown { org = o, selectedFlag = true });
                    } else {
                        ccodd.Add(new CCClientOrganizationDropDown { org = o, selectedFlag = false });
                    }
                }
                return PartialView(ccodd);
            } else return RedirectToAction("Index", "Login");
        }
        
        [HttpGet]
        public ActionResult Edit(Guid id) {
            if (Session["loggedInUser"] != null) {
                return View(EverythingToDoWith_CCClientOrganizations.getOrganizationANDContactCardByID(id, (((CCPerson)Session["loggedInUser"]).clientID)));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Edit(CCClientOrganizationANDCCGenericContactCard o) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(o);
                }
                try {
                    EverythingToDoWith_CCClientOrganizations.updateOrganization(o);
                    ViewData["message"] = "Organization updated successfully";
                    ViewData["type"] = CustomEnums.NotificationType.SUCCESS;
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                }
                return View(o);
            } else return RedirectToAction("Index", "Login");
        }

    }
}
