using System;
using System.Collections.Generic;
using System.Collections.Specialized;   //For ListDictionary
using System.Linq;
using System.Web;
using CloudClaims.Model;
using System.Web.Mvc;
using CloudClaims.Data.Portal;
using System.Collections;

namespace CloudClaims.ViewModels {
    public class DropdownListCollection {
        public List<IEnumerable<SelectListItem>> sex { get; set; }
    }
    public class vmClaimEditLite {

        public CCClaim ccClaim { get; set; }

        private List<vmLabelValuePair> _claimDateTypes = new List<vmLabelValuePair>();
        public List<vmLabelValuePair> claimDateTypes {
            get {
                foreach (CCClaimDateType dt in EverythingToDoWith_CCClaimDateTypes.getAllClaimDateTypesByClientID(ccClaim.clientID, true)) {
                    _claimDateTypes.Add(new vmLabelValuePair { id = dt.claimDateTypeID.ToString(), label = dt.claimDateType, value = String.Empty });
                }
                return _claimDateTypes;
            }
            set {
                _claimDateTypes = value;
            }
        }
       
        public List<CCClaimDate> claimDates { get; set; }

        #region CSS STUFF
        private List<string> _claimDatesCSS = new List<string>();
        public List<string> claimDatesCSS {
            get {
                foreach (CCClaimDate d in claimDates) {
                    if (EverythingToDoWith_CCClaimDateTypes.getClaimDateTypeByID(ccClaim.clientID, d.claimDateTypeID).claimDateTypeDisabled) {
                        _claimDatesCSS.Add(" disabled-background ");
                    } else {
                        _claimDatesCSS.Add(String.Empty);
                    }
                }
                return _claimDatesCSS;
            }
        }

        private List<string> _claimTextboxCSS = new List<string>();
        public List<string> claimTextboxCSS {
            get {
                foreach (var de in claimTextboxes) {
                    if (ETDW_CCSetting_ClaimTextboxTypes.getTextboxTypeByID(ccClaim.clientID, de.Key).textboxDisabled.Value) {
                        _claimTextboxCSS.Add(" disabled-background ");
                    } else {
                        _claimTextboxCSS.Add(String.Empty);
                    }
                }
                return _claimTextboxCSS;
            }
        }

        private List<string> _claimCheckboxCSS = new List<string>();
        public List<string> claimCheckboxCSS {
            get {
                foreach (var de in claimCheckboxes) {

                    if (ETDW_CCSetting_ClaimCheckboxTypes.getCheckboxTypeByID(ccClaim.clientID, de.Key).checkboxDisabled.Value) {
                        _claimCheckboxCSS.Add(" disabled-background ");
                    } else {
                        _claimCheckboxCSS.Add(String.Empty);
                    }
                }
                return _claimCheckboxCSS;
            }
        }
        #endregion

        private IEnumerable<CCSetting_ClaimPersonnelTypes> _claimPersonnelTypes = new List<CCSetting_ClaimPersonnelTypes>();
        public IEnumerable<CCSetting_ClaimPersonnelTypes> claimPersonnelTypes { get { return _claimPersonnelTypes; } set { _claimPersonnelTypes = value; } }

        public IEnumerable<CCClaimPerson> claimPersons { get; set; }

        private Dictionary<int, List<SelectListItem>> _solvedClaimDropdownsSettings = new Dictionary<int, List<SelectListItem>>();
        public Dictionary<int, List<SelectListItem>> SolvedClaimDropdownSettings { get { return _solvedClaimDropdownsSettings; } set { _solvedClaimDropdownsSettings = value; } }

        private Dictionary<int, int> _solvedClaimDropdownSelectedValues = new Dictionary<int, int>();
        public Dictionary<int, int> SolvedClaimDropdownSelectedValues { get { return _solvedClaimDropdownSelectedValues; } set { _solvedClaimDropdownSelectedValues = value; } }

        private Dictionary<int, string> _CCSetting_claimTextboxTypes = new Dictionary<int, string>();
        public Dictionary<int, string> CCSetting_ClaimTextboxTypes { get { return _CCSetting_claimTextboxTypes; } set { _CCSetting_claimTextboxTypes = value; } }

        private Dictionary<int, string> _CCSetting_claimCheckboxTypes = new Dictionary<int, string>();
        public Dictionary<int, string> CCSetting_ClaimCheckboxTypes { get { return _CCSetting_claimCheckboxTypes; } set { _CCSetting_claimCheckboxTypes = value; } }

        private Dictionary<int, string> _claimTextboxes = new Dictionary<int, string>();
        public Dictionary<int, string> claimTextboxes { get { return _claimTextboxes; } set { _claimTextboxes = value; } }

        private Dictionary<int, bool> _claimCheckboxes = new Dictionary<int, bool>();
        public Dictionary<int, bool> claimCheckboxes { get { return _claimCheckboxes; } set { _claimCheckboxes = value; } }

        public vmCustomForm vmCustomForm { get; set; }
        public List<vmGridster> vmGridster { get; set; }
    }
}