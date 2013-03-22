using CloudClaims.Data;
using CloudClaims.Data.Portal;
using CloudClaims.Model;
using CloudClaims.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CloudClaims.Controllers {
    public class HomeController : Controller {
        [HttpGet]
        public ActionResult Index() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult LeftNav() {

            vmTopNav vm = new vmTopNav((CCPerson)Session["loggedInUser"]);
            return PartialView(vm);

        }

        public ActionResult TopNav() {

            vmTopNav vm = new vmTopNav((CCPerson)Session["loggedInUser"]);
            return PartialView(vm);

        }

        [HttpGet]
        public ActionResult ManageClaims() {
            if (Session["loggedInUser"] != null) {
                try {
                    List<vmManageClaims> vmManageClaims = new List<vmManageClaims>();
                    foreach (CCClaim c in EverythingToDoWith_CCClaims.getAllClaimsFor(((CCPerson)Session["loggedInUser"]).clientID)) {
                        vmManageClaims vm = new vmManageClaims();
                        vm.ccClaim = c;
                        vmManageClaims.Add(vm);
                    }
                    return View(vmManageClaims);
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult ClaimAdd() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult ClaimAddLite() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ClaimAddLite(CCClaim c) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(c);
                }
                try {
                    c.clientID = ((CCPerson)Session["loggedInUser"]).clientID;
                    #region Add main claim record
                    Guid g = EverythingToDoWith_CCClaims.addClaim(c);
                    #endregion
                    #region Attach claim date type records
                    foreach (CCClaimDateType dt in EverythingToDoWith_CCClaimDateTypes.getAllClaimDateTypesByClientID(c.clientID, true)) {
                        EverythingToDoWith_CCClaimDates.addClaimDateRecord(new CCClaimDate {
                            clientID = c.clientID,
                            claimGUID = g,
                            claimDateTypeID = dt.claimDateTypeID,
                            claimDateStamp = null
                        });
                    }
                    #endregion

                    ViewData["message"] = String.Format("Claim {0} has been succesfully created", c.claimAlias1);
                    ViewData["type"] = CustomEnums.NotificationType.SUCCESS;
                    //return View(c);
                    return RedirectToAction("ClaimEditLite", "Home", new { guid = g.ToString() });
                } catch (DbEntityValidationException dbEx) {
                    StringBuilder s = new StringBuilder();
                    foreach (var validationErrors in dbEx.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            s.Append(validationError.PropertyName + ": " + validationError.ErrorMessage);
                        }
                    }
                    ViewData["message"] = s;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult ClaimEditLiteJSON(string guid) {
            #region Load the claim
            CCClaim claim = EverythingToDoWith_CCClaims.getClaimByGuid(new Guid(guid), ((CCPerson)Session["loggedInUser"]).clientID);
            #endregion
            vmClaimEditLiteJSON vm = new vmClaimEditLiteJSON();
            #region Load claim dates
            foreach (CCClaimDate cd in EverythingToDoWith_CCClaimDates.getClaimDateRecords(((CCPerson)Session["loggedInUser"]).clientID, new Guid(guid))) {
                vm.claimDates.Add(new vmLabelValuePair {
                    id = cd.claimDateID.ToString(),
                    label = cd.claimDateTypeID.ToString(),
                    value = cd.claimDateStamp.HasValue ? cd.claimDateStamp.Value.ToShortDateString() : String.Empty
                });
            }
            #endregion
            #region Load claim textboxes
            foreach (CCClaimTextboxValue ct in ETDW_CCClaimTextboxes.getClaimTextboxValues(((CCPerson)Session["loggedInUser"]).clientID, claim.claimID)) {
                vm.claimTextboxes.Add(new vmLabelValuePair { id = ct.recordID.ToString(), label = ct.textboxTypeID.ToString(), value = ct.textboxValue });
            }
            #endregion
            #region Load claim checkboxes
            foreach (CCClaimCheckboxValue cb in ETDW_CCClaimCheckboxes.getClaimCheckboxValues(((CCPerson)Session["loggedInUser"]).clientID, claim.claimID)) {
                vm.claimCheckboxes.Add(new vmLabelValuePair { id = cb.recordID.ToString(), label = cb.checkboxTypeID.ToString(), value = cb.@checked.ToString() });
            }
            #endregion
            #region Load claim dropdowns
            CCSetting_CustomForms loadedCF = ETDW_CCSetting_CustomForms.getCustomFormByFormType(((CCPerson)Session["loggedInUser"]).clientID, CustomEnums.FormType.FORMTYPE_CLAIM);
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<vmGridster> gridster = new List<vmGridster>();
            gridster = (List<vmGridster>)js.Deserialize(loadedCF.serializedForm, typeof(List<vmGridster>));
            foreach (vmGridster g in gridster) {
                switch (g.elementtype) {
                    case "dropdown": {
                        vmDropdownValueCollection dvc = new vmDropdownValueCollection();
                        dvc.dropdownTypeID = Convert.ToInt32(CloudClaims.Utils.RemoveNonNumericChars(g.elementtypeid));
                        List<CCSetting_ClaimDropdownValues> cdv = EverythingToDoWith_CCSetting_ClaimDropdownValues.getClaimDropdownValues(((CCPerson)Session["loggedInUser"]).clientID, dvc.dropdownTypeID);
                        foreach(CCSetting_ClaimDropdownValues dv in cdv) {
                            dvc.dropdownValues.Add(new SelectListItem { Text = dv.ddValueItem, Value = dv.ddValueID.ToString() });
                        }
                        vm.claimDropdowns.Add(dvc);
                        break;
                        }
                }
            }
            #endregion
            #region Load claim dropdown selected values
            foreach (CCClaimDropdownValue cdv in claim.CCClaimDropdownValues) {
                vmDropdownValueCollection instance = vm.claimDropdowns.Where(x => x.dropdownTypeID == cdv.dropdownTypeID).FirstOrDefault();
                if (instance != null) {
                    instance.dropdownValues.Where(x => x.Value == cdv.dropdownValueID.ToString()).SingleOrDefault().Selected = true;
                }
            }
            #endregion
            return Json(vm);
        }

        [HttpPost]
        public JsonResult ClaimEditListJSONSave(string guid, vmClaimEditLiteJSON vm) {
            #region Load the claim
            CCClaim claim = EverythingToDoWith_CCClaims.getClaimByGuid(new Guid(guid), ((CCPerson)Session["loggedInUser"]).clientID);
            #endregion
            #region Update claim dates
            foreach (vmLabelValuePair cd in vm.claimDates) {
                CCClaimDate d = EverythingToDoWith_CCClaimDates.getClaimDateByID(((CCPerson)Session["loggedInUser"]).clientID, new Guid(guid), Convert.ToInt32(cd.id));
                if (d.claimDateStamp != Convert.ToDateTime(cd.value)) {
                    d.claimDateStamp = Convert.ToDateTime(cd.value);
                    EverythingToDoWith_CCClaimDates.updateClaimDateRecord(d);
                }
            }
            #endregion
            #region Load active claim personnel type
            //vm.claimPersonnelTypes = EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getAllClaimPersonnelTypes((((CCPerson)Session["loggedInUser"]).clientID), true);
            //vm.claimPersons = EverythingToDoWith_CCClaimPersons.getAllClaimPersons((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID);
            #endregion
            #region Update claim dropdown type selected values
            //foreach (var de in vm.SolvedClaimDropdownSelectedValues) {
            //    if (!String.IsNullOrEmpty(de.Value.ToString()))
            //        if (de.Value > 0)
            //            EverythingToDoWith_CCClaimDropdownValues.updateClaimDropdownValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, de.Key, de.Value);
            //}
            #endregion
            #region Update claim textbox type selected values
            //foreach (var tb in vm.claimTextboxes) {
            //    ETDW_CCClaimTextboxes.updateClaimTextboxValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, tb.Key, tb.Value);
            //}
            #endregion
            #region Update claim checkbox type selected values
            //foreach (var cb in vm.claimCheckboxes) {
            //    ETDW_CCClaimCheckboxes.updateClaimCheckboxValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, cb.Key, cb.Value);
            //}
            #endregion
            return Json(String.Empty);
        }

        [HttpGet]
        public ActionResult ClaimEditLite(Guid guid, int cf = 0) {
            if (Session["loggedInUser"] != null) {
                try {
                    vmClaimEditLite vmClaimEditLite = new vmClaimEditLite();
                    #region Load basic claim
                    vmClaimEditLite.ccClaim = EverythingToDoWith_CCClaims.getClaimByGuid(guid, ((CCPerson)Session["loggedInUser"]).clientID);
                    #endregion
                    #region Register latest claim date type settings
                    vmClaimEditLite.claimDates = EverythingToDoWith_CCClaimDates.getClaimDateRecords(((CCPerson)Session["loggedInUser"]).clientID, guid).ToList();
                    foreach (CCClaimDateType dt in EverythingToDoWith_CCClaimDateTypes.getAllClaimDateTypesByClientID(((CCPerson)Session["loggedInUser"]).clientID, false)) {
                        //Check if the claim already has a record for this active event date type, if not then create it
                        if (vmClaimEditLite.claimDates.Where(d => d.claimDateTypeID == dt.claimDateTypeID).Count() == 0) {
                            CCClaimDate temp = new CCClaimDate {
                                clientID = vmClaimEditLite.ccClaim.clientID,
                                claimGUID = guid,
                                claimDateTypeID = dt.claimDateTypeID,
                                claimDateStamp = null
                            };
                            EverythingToDoWith_CCClaimDates.addClaimDateRecord(temp);
                            vmClaimEditLite.claimDates.Add(temp);
                        }
                    }
                    #endregion
                    #region Load active claim personnel type
                    vmClaimEditLite.claimPersonnelTypes = EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getAllClaimPersonnelTypes((((CCPerson)Session["loggedInUser"]).clientID), true);
                    vmClaimEditLite.claimPersons = EverythingToDoWith_CCClaimPersons.getAllClaimPersons((((CCPerson)Session["loggedInUser"]).clientID), vmClaimEditLite.ccClaim.claimID);
                    #endregion
                    #region Load active claim dropdown types
                    //int ctr = 0;
                    foreach (CCSetting_ClaimDropdownTypes cdt in EverythingToDoWith_CCSetting_ClaimDropdownTypes.getAllClaimDropdownTypes(((CCPerson)Session["loggedInUser"]).clientID)) {
                        List<SelectListItem> tempList = new List<SelectListItem>();
                        tempList.Add(new SelectListItem { Text = String.Empty, Value = "0" });
                        foreach (CCSetting_ClaimDropdownValues cdv in cdt.CCSetting_ClaimDropdownValues) {
                            SelectListItem tempSLI = new SelectListItem { Value = cdv.ddValueID.ToString(), Text = cdv.ddValueItem };
                            tempList.Add(tempSLI);

                        }
                        vmClaimEditLite.SolvedClaimDropdownSettings.Add(cdt.dropdownTypeID, tempList);
                        vmClaimEditLite.SolvedClaimDropdownSelectedValues.Add(cdt.dropdownTypeID, 0);   //Initialize place holder for user selection
                        //ctr++;
                    }
                    #endregion
                    #region Load existing claimDropdownValues and mark the ones above as selected if they exist into the initializer from above
                    foreach (CCClaimDropdownValue cdv in vmClaimEditLite.ccClaim.CCClaimDropdownValues) {
                        if (vmClaimEditLite.SolvedClaimDropdownSelectedValues.ContainsKey(cdv.dropdownTypeID)) {
                            vmClaimEditLite.SolvedClaimDropdownSelectedValues[cdv.dropdownTypeID] = cdv.dropdownValueID;
                            foreach (SelectListItem sli in vmClaimEditLite.SolvedClaimDropdownSettings[cdv.dropdownTypeID]) {
                                if (sli.Value == cdv.dropdownValueID.ToString()) {
                                    sli.Selected = true;
                                }
                            }
                        }
                    }
                    #endregion
                    #region Load active claim textbox types
                    foreach (CCSetting_ClaimTextboxTypes ctt in ETDW_CCSetting_ClaimTextboxTypes.getAllClaimTextboxTypes(((CCPerson)Session["loggedInUser"]).clientID, false)) {
                        vmClaimEditLite.CCSetting_ClaimTextboxTypes.Add(ctt.textboxTypeID, ctt.textboxName);
                        vmClaimEditLite.claimTextboxes.Add(ctt.textboxTypeID, String.Empty);
                    }
                    #endregion
                    #region Load existing claimTextboxValues and mark the ones above as populated if they exist in the initializer from above
                    foreach (CCClaimTextboxValue tbv in vmClaimEditLite.ccClaim.CCClaimTextboxValues) {
                        vmClaimEditLite.claimTextboxes[tbv.textboxTypeID] = tbv.textboxValue;
                    }
                    #endregion
                    #region Load active claim checkbox types
                    foreach (CCSetting_ClaimCheckboxTypes ccbt in ETDW_CCSetting_ClaimCheckboxTypes.getAllClaimCheckboxTypes(((CCPerson)Session["loggedInUser"]).clientID, false)) {
                        vmClaimEditLite.CCSetting_ClaimCheckboxTypes.Add(ccbt.checkboxTypeID, ccbt.checkboxName);
                        vmClaimEditLite.claimCheckboxes.Add(ccbt.checkboxTypeID, false);
                    }
                    #endregion
                    #region Load existing claimCheckboxValues and mark the ones above as populated if they exist in the initializer from above
                    foreach (CCClaimCheckboxValue cbv in vmClaimEditLite.ccClaim.CCClaimCheckboxValues) {
                        vmClaimEditLite.claimCheckboxes[cbv.checkboxTypeID] = cbv.@checked;
                    }
                    #endregion

                    #region Load custom form
                    if (cf == 1) {
                        CCSetting_CustomForms loadedCF = ETDW_CCSetting_CustomForms.getCustomFormByFormType(((CCPerson)Session["loggedInUser"]).clientID, CustomEnums.FormType.FORMTYPE_CLAIM);
                        if (loadedCF != null) {
                            vmClaimEditLite.vmCustomForm = new vmCustomForm();
                            vmClaimEditLite.vmCustomForm.clientID = loadedCF.clientID;
                            vmClaimEditLite.vmCustomForm.formID = loadedCF.formID;
                            vmClaimEditLite.vmCustomForm.formName = loadedCF.formName;
                            return View("ClaimEditCustomForm", vmClaimEditLite);
                        } else {
                            return View(vmClaimEditLite);
                        }
                    } else {
                    #endregion
                        return View(vmClaimEditLite);
                    }
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View();
                }
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ClaimEditLite(vmClaimEditLite vm) {
            if (Session["loggedInUser"] != null) {
                if (!ModelState.IsValid) {
                    ViewData["message"] = Server.HtmlEncode(Utils.ServerSideValidation(ModelState));
                    ViewData["type"] = CustomEnums.NotificationType.VALIDATION_ERROR;
                    return View(vm);
                }
                try {
                    //Update claim record
                    EverythingToDoWith_CCClaims.updateClaim(vm.ccClaim);
                    #region Update claim dates
                    foreach (CCClaimDate d in vm.claimDates) {
                        EverythingToDoWith_CCClaimDates.updateClaimDateRecord(d);
                    }
                    #endregion
                    #region Load active claim personnel type
                    //vm.claimPersonnelTypes = EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getAllClaimPersonnelTypes((((CCPerson)Session["loggedInUser"]).clientID), true);
                    //vm.claimPersons = EverythingToDoWith_CCClaimPersons.getAllClaimPersons((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID);
                    #endregion
                    #region Update claim dropdown type selected values
                    foreach (var de in vm.SolvedClaimDropdownSelectedValues) {
                        if (!String.IsNullOrEmpty(de.Value.ToString()))
                            if (de.Value > 0)
                                EverythingToDoWith_CCClaimDropdownValues.updateClaimDropdownValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, de.Key, de.Value);
                    }
                    #endregion
                    #region Update claim textbox type selected values
                    foreach (var tb in vm.claimTextboxes) {
                        ETDW_CCClaimTextboxes.updateClaimTextboxValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, tb.Key, tb.Value);
                    }
                    #endregion
                    #region Update claim checkbox type selected values
                    foreach (var cb in vm.claimCheckboxes) {
                        ETDW_CCClaimCheckboxes.updateClaimCheckboxValue((((CCPerson)Session["loggedInUser"]).clientID), vm.ccClaim.claimID, cb.Key, cb.Value);
                    }
                    #endregion
                    TempData["message"] = "Claim updated succesfully";
                    TempData["type"] = CustomEnums.NotificationType.INFORMATION;
                    //return View(vm);
                    return RedirectToAction("ClaimEditLite", new { guid = vm.ccClaim.claimGUID });
                } catch (Exception e) {
                    ViewData["message"] = e.Message;
                    ViewData["type"] = CustomEnums.NotificationType.FAILURE;
                    return View(vm);
                }
            } else return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public JsonResult AddClaimPerson(int claimID, int personID, int claimPersonnelTypeID) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCClaimPersons.addClaimPerson(((CCPerson)Session["loggedInUser"]).clientID, claimID, personID, claimPersonnelTypeID);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Claim personnel associated successfully")
                    });
                } catch (Exception e) {
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.FAILURE,
                        message = e.Message
                    });
                }
            } else return Json(new vmNotifications {
                type = Data.CustomEnums.NotificationType.FAILURE,
                message = "Must be logged in!"
            });

        }

        public ActionResult ClaimPhotos(Guid guid) {
            if (Session["loggedInUser"] != null) {
                vmClaimPhotos vm = new vmClaimPhotos();
                CCClaim claim = EverythingToDoWith_CCClaims.getClaimByGuid(guid, ((CCPerson)Session["loggedInUser"]).clientID);
                vm.claimPhotos = claim.CCClaimMedias.Where(x => x.mediaType == CustomEnums.MEDIATYPE_PHOTO);
                return View(vm);
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult ClaimDocuments(Guid guid) {
            if (Session["loggedInUser"] != null) {
                vmClaimDocuments vm = new vmClaimDocuments();
                CCClaim claim = EverythingToDoWith_CCClaims.getClaimByGuid(guid, ((CCPerson)Session["loggedInUser"]).clientID);
                vm.claimDocuments = claim.CCClaimMedias.Where(x => x.mediaType == CustomEnums.MEDIATYPE_DOCUMENT);
                return View(vm);
            } else return RedirectToAction("Index", "Login");
        }

        public ActionResult ClaimDocumentEdit(int id) {
            if (Session["loggedInUser"] != null) {
                vmClaimDocumentEdit vm = new vmClaimDocumentEdit();
                vm.claimDocument = ETDW_CCClaimMedia.GetClaimMediaByID(((CCPerson)Session["loggedInUser"]).clientID, id);
                return View();
            } else return RedirectToAction("Index", "Login");
        }

    }
}
