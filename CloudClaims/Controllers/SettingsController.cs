using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudClaims.Data.Portal;
using CloudClaims.Model;
using CloudClaims.ViewModels;
using System.Data.Entity.Validation;
using System.Text;
using System.Diagnostics;
using CloudClaims.Data;
using System.Web.Script.Serialization;

namespace CloudClaims.Controllers {
    public class SettingsController : Controller {
        [HttpGet]
        public ActionResult Index() {
            if (Session["loggedInUser"] != null) {
                return View();
            } else return RedirectToAction("Index", "Login");
        }

        #region CLAIM DATE TYPES
        [HttpGet]
        public ActionResult ClaimDateTypes() {
            if (Session["loggedInUser"] != null) {
                return PartialView(EverythingToDoWith_CCClaimDateTypes.getAllClaimDateTypesByClientID(((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult AddClaimDateType(string claimDateType) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCClaimDateTypes.addClaimDateType(((CCPerson)Session["loggedInUser"]).clientID, claimDateType);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Event date type [{0}] created successfully", claimDateType)
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


        [HttpPost]
        public JsonResult EditClaimDateType(CCClaimDateType d) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCClaimDateTypes.updateClaimDateType(d);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Event date type [{0}] updated successfully", d.claimDateType)
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

        [HttpPost]
        public JsonResult DisableClaimDateType(int claimDateTypeID, bool state) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCClaimDateTypes.disableClaimDateType(((CCPerson)Session["loggedInUser"]).clientID, claimDateTypeID, state);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Event date type {0} successfully", state.ToString().Replace("False", "enabled").Replace("True", "disabled"))
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


        [HttpPost]
        public ActionResult DeleteClaimDateType(int id) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCClaimDateTypes.deleteClaimDateType(((CCPerson)Session["loggedInUser"]).clientID, id);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Event date type deleted")
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

        #endregion

        #region CLAIM PERSONNEL TYPES
        [HttpGet]
        public ActionResult CCSetting_ClaimPersonnelTypes() {
            if (Session["loggedInUser"] != null) {
                return PartialView(EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getAllClaimPersonnelTypes(((CCPerson)Session["loggedInUser"]).clientID, false));
            } else return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult DeleteClaimPersonnelType(int id) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimPersonnelTypes.deleteClaimPersonnelType(((CCPerson)Session["loggedInUser"]).clientID, id);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Claim personnel type deleted")
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


        [HttpPost]
        public JsonResult AddClaimPersonnelType(string claimPersonnelType) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimPersonnelTypes.addClaimPersonnelType(((CCPerson)Session["loggedInUser"]).clientID, claimPersonnelType);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Claim personnel type [{0}] created successfully", claimPersonnelType)
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

        [HttpPost]
        public JsonResult DisableClaimPersonnelType(int claimPersonnelTypeID, bool state) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimPersonnelTypes.disableClaimPersonnelType(((CCPerson)Session["loggedInUser"]).clientID, claimPersonnelTypeID, state);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Personnel type {0} successfully", state.ToString().Replace("False", "enabled").Replace("True", "disabled"))
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
        #endregion

        #region CLAIM DROPDOWN TYPES

        [HttpGet]
        public ActionResult CCSetting_ClaimDropdownTypes() {
            if (Session["loggedInUser"] != null) {
                return PartialView(EverythingToDoWith_CCSetting_ClaimDropdownTypes.getAllClaimDropdownTypes(((CCPerson)Session["loggedInUser"]).clientID, false));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult DisableClaimDropdownType(int claimDropdownTypeID, bool state) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimDropdownTypes.disableClaimDropdownType(((CCPerson)Session["loggedInUser"]).clientID, claimDropdownTypeID, state);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Dropdown type {0} successfully", state.ToString().Replace("False", "enabled").Replace("True", "disabled"))
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

        [HttpPost]
        public JsonResult AddClaimDropdownType(string claimDropdownType) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimDropdownTypes.addClaimDropdownType(((CCPerson)Session["loggedInUser"]).clientID, claimDropdownType);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Claim dropdown type [{0}] created successfully", claimDropdownType)
                    });
                } catch (DbEntityValidationException dbEx) {
                    StringBuilder s = new StringBuilder();
                    foreach (var validationErrors in dbEx.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            s.Append(validationError.PropertyName + ": " + validationError.ErrorMessage);
                        }
                    }
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.VALIDATION_ERROR,
                        message = dbEx.Message
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

        [HttpPost]
        public ActionResult DeleteClaimDropdownType(int id) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimDropdownTypes.deleteClaimDropdownType(((CCPerson)Session["loggedInUser"]).clientID, id);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Claim dropdown type deleted")
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

        public ActionResult CCSetting_ClaimDropdownValues(int dropdownTypeID) {
            if (Session["loggedInUser"] != null) {
                try {
                    List<vmCCSetting_ClaimDropdownValue> result = new List<vmCCSetting_ClaimDropdownValue>();
                    foreach (CCSetting_ClaimDropdownValues cdv in EverythingToDoWith_CCSetting_ClaimDropdownTypes.getDropdownTypeByID(((CCPerson)Session["loggedInUser"]).clientID, dropdownTypeID).CCSetting_ClaimDropdownValues) {
                        result.Add(new vmCCSetting_ClaimDropdownValue {
                            ddlValueID = cdv.ddValueID,
                            ddlValueItem = cdv.ddValueItem,
                            sortOrder = cdv.sortOrder ?? 0,
                            itemDisabled = cdv.itemDisabled,
                            itemDeleted = cdv.itemDeleted
                        });
                    }
                    return PartialView(result);
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

        [HttpPost]
        public JsonResult AddCCSetting_ClaimDropdownValues(int dropdownTypeID, string dropdownValue) {
            if (Session["loggedInUser"] != null) {
                try {
                    EverythingToDoWith_CCSetting_ClaimDropdownValues.addClaimDropdownValue(((CCPerson)Session["loggedInUser"]).clientID, dropdownTypeID, dropdownValue);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Dropdown value [{0}] created successfully", dropdownValue)
                    });
                } catch (DbEntityValidationException dbEx) {
                    StringBuilder s = new StringBuilder();
                    foreach (var validationErrors in dbEx.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            s.Append(validationError.PropertyName + ": " + validationError.ErrorMessage);
                        }
                    }
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.VALIDATION_ERROR,
                        message = dbEx.Message
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
        #endregion

        #region CLAIM TEXTBOX TYPES
        [HttpGet]
        public ActionResult CCSetting_ClaimTextboxTypes() {
            if (Session["loggedInUser"] != null) {
                return PartialView(ETDW_CCSetting_ClaimTextboxTypes.getAllClaimTextboxTypes(((CCPerson)Session["loggedInUser"]).clientID, false));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult DeleteClaimTextboxType(int id) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimTextboxTypes.deleteClaimTextboxType(((CCPerson)Session["loggedInUser"]).clientID, id);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Claim textbox type deleted")
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

        [HttpPost]
        public JsonResult AddClaimTextboxType(string claimTextboxType) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimTextboxTypes.addClaimTextboxType(((CCPerson)Session["loggedInUser"]).clientID, claimTextboxType);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Claim textbox type [{0}] created successfully", claimTextboxType)
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

        [HttpPost]
        public JsonResult DisableClaimTextboxType(int claimTextboxTypeID, bool state) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimTextboxTypes.disableClaimTextboxType(((CCPerson)Session["loggedInUser"]).clientID, claimTextboxTypeID, state);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Textbox type {0} successfully", state.ToString().Replace("False", "enabled").Replace("True", "disabled"))
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
        #endregion

        #region CLAIM CHECKBOX TYPES
        [HttpGet]
        public ActionResult CCSetting_ClaimCheckboxTypes() {
            if (Session["loggedInUser"] != null) {
                return PartialView(ETDW_CCSetting_ClaimCheckboxTypes.getAllClaimCheckboxTypes(((CCPerson)Session["loggedInUser"]).clientID, false));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult DisableClaimCheckboxType(int claimCheckboxTypeID, bool state) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimCheckboxTypes.disableClaimCheckboxType(((CCPerson)Session["loggedInUser"]).clientID, claimCheckboxTypeID, state);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Checkbox type {0} successfully", state.ToString().Replace("False", "enabled").Replace("True", "disabled"))
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

        [HttpPost]
        public JsonResult AddClaimCheckboxType(string claimCheckboxType) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimCheckboxTypes.addClaimCheckboxType(((CCPerson)Session["loggedInUser"]).clientID, claimCheckboxType);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Claim checkbox type [{0}] created successfully", claimCheckboxType)
                    });
                } catch (DbEntityValidationException dbEx) {
                    StringBuilder s = new StringBuilder();
                    foreach (var validationErrors in dbEx.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            s.Append(validationError.PropertyName + ": " + validationError.ErrorMessage);
                        }
                    }
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.VALIDATION_ERROR,
                        message = s.ToString()
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

        [HttpPost]
        public ActionResult DeleteClaimCheckboxType(int id) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_ClaimCheckboxTypes.deleteClaimCheckboxType(((CCPerson)Session["loggedInUser"]).clientID, id);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.INFORMATION,
                        message = String.Format("Claim checkbox type deleted")
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
        #endregion

        #region CUSTOM FORM BUILDER
        [HttpGet]
        public ActionResult CCSetting_CustomFormBuilder() {
            if (Session["loggedInUser"] != null) {
                IEnumerable<CCSetting_CustomForms> existingForms = ETDW_CCSetting_CustomForms.getAllCustomForms(((CCPerson)Session["loggedInUser"]).clientID);
                List<SelectListItem> unusedFormTypes = new List<SelectListItem>();
                foreach (CustomEnums.FormType ft in Enum.GetValues(typeof(CustomEnums.FormType))) {
                    string tempFT = ft.ToString();
                    if (!existingForms.Any(f => f.formType == tempFT)) {
                        SelectListItem sli = new SelectListItem { Text = tempFT, Value = tempFT };
                        unusedFormTypes.Add(sli);
                    }
                }
                return View(unusedFormTypes);
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult AddCustomForm(vmCustomForm cf) {
            if (Session["loggedInUser"] != null) {
                try {
                    int formID = ETDW_CCSetting_CustomForms.addCustomForm(((CCPerson)Session["loggedInUser"]).clientID, cf.formType, cf.formName, cf.serializedForm);

                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Custom form [{0}] created successfully", cf.formName)
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

        [HttpPost]
        public JsonResult UpdateCustomform(vmCustomForm cf) {
            if (Session["loggedInUser"] != null) {
                try {
                    ETDW_CCSetting_CustomForms.updateCustomForm(((CCPerson)Session["loggedInUser"]).clientID, cf.formID, cf.formName, cf.serializedForm);
                    return Json(new vmNotifications {
                        type = Data.CustomEnums.NotificationType.SUCCESS,
                        message = String.Format("Custom form [{0}] updated successfully", cf.formName)
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

        [HttpPost]
        public JsonResult GetCustomForm(int formID) {
            if (Session["loggedInUser"] != null) {
                try {
                    CCSetting_CustomForms cf = ETDW_CCSetting_CustomForms.getCustomFormByFormID(((CCPerson)Session["loggedInUser"]).clientID, formID);
                    vmCustomForm vmCF = new vmCustomForm { formID = cf.formID, serializedForm = cf.serializedForm, formName = cf.formName };
                    return Json(vmCF);
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

        [HttpPost]
        public JsonResult GetCustomFormGridsterObject(int formID) {
            if (Session["loggedInUser"] != null) {
                try {
                    CCSetting_CustomForms cf = ETDW_CCSetting_CustomForms.getCustomFormByFormID(((CCPerson)Session["loggedInUser"]).clientID, formID);
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    List<vmGridster> gridster = new List<vmGridster>();
                    gridster = (List<vmGridster>)js.Deserialize(cf.serializedForm, typeof(List<vmGridster>));
                    return Json(gridster);
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

        [HttpPost]
        public JsonResult GetFormElementTitle(string elementType, int elementTypeID) {
            if (Session["loggedInUser"] != null) {
                try {
                    string elementTitle = String.Empty;
                    switch (elementType) {
                        case CustomEnums.CCSetting_ClaimCheckboxTypes: {
                                elementTitle = ETDW_CCSetting_ClaimCheckboxTypes.getCheckboxTypeName(((CCPerson)Session["loggedInUser"]).clientID, elementTypeID);
                                break;
                            }
                        case CustomEnums.CCSetting_ClaimTextboxTypes: {
                                elementTitle = ETDW_CCSetting_ClaimTextboxTypes.getTextboxTypeName(((CCPerson)Session["loggedInUser"]).clientID, elementTypeID);
                                break;
                            }
                        case CustomEnums.CCSetting_ClaimDropdownTypes: {
                                elementTitle = EverythingToDoWith_CCSetting_ClaimDropdownTypes.getDropdownTypeName(((CCPerson)Session["loggedInUser"]).clientID, elementTypeID);
                                break;
                            }
                        case CustomEnums.CCSetting_ClaimDateTypes: {
                                elementTitle = EverythingToDoWith_CCClaimDateTypes.getClaimDateTypeByID(((CCPerson)Session["loggedInUser"]).clientID, elementTypeID).claimDateType;
                                break;
                            }
                        case CustomEnums.CCSetting_ClaimPersonnelTypes: {
                            elementTitle = EverythingToDoWith_CCSetting_ClaimPersonnelTypes.getClaimPersonnelTypeNameByID(((CCPerson)Session["loggedInUser"]).clientID, elementTypeID);
                                break;
                            }
                    }
                    elementTitle = (elementTitle.Length > 0) ? elementTitle : "[Title Unavailable]";
                    return Json(elementTitle);
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

        [HttpGet]
        public ActionResult CCSetting_CustomFormBuilderManage() {
            if (Session["loggedInUser"] != null) {

                return View(ETDW_CCSetting_CustomForms.getAllCustomForms(((CCPerson)Session["loggedInUser"]).clientID));
            } else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public JsonResult ReturnUnusedFormTypes() {
            if (Session["loggedInUser"] != null) {
                try {
                    IEnumerable<CCSetting_CustomForms> existingForms = ETDW_CCSetting_CustomForms.getAllCustomForms(((CCPerson)Session["loggedInUser"]).clientID);
                    List<string> unusedFormTypes = new List<string>();
                    foreach (CustomEnums.FormType ft in Enum.GetValues(typeof(CustomEnums.FormType))) {
                        string tempFT = ft.ToString();
                        if (!existingForms.Any(f => f.formType == tempFT )) {
                            unusedFormTypes.Add(tempFT);
                        }
                    }
                    return Json(unusedFormTypes);
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
        #endregion
    }
}