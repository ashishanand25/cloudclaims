using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaims.ViewModels {

    public class vmDropdownValueCollection {
        public int dropdownTypeID { get; set; }
        public List<SelectListItem> dropdownValues { get; set; }

        public vmDropdownValueCollection() {
            this.dropdownValues = new List<SelectListItem>();
        }
    }

    public class vmClaimEditLiteJSON {
        private List<vmLabelValuePair> _claimDates = new List<vmLabelValuePair>();
        public List<vmLabelValuePair> claimDates { get { return _claimDates; } set { _claimDates = value; } }

        //private IEnumerable<vmLabelValuePair> _claimPersonnelTypes = new List<vmLabelValuePair>();
        //public IEnumerable<vmLabelValuePair> claimPersonnelTypes { get { return _claimPersonnelTypes; } set { _claimPersonnelTypes = value; } }

        private List<vmLabelValuePair> _claimTextboxes = new List<vmLabelValuePair>();
        public List<vmLabelValuePair> claimTextboxes { get { return _claimTextboxes; } set { _claimTextboxes = value; } }

        private List<vmLabelValuePair> _claimCheckboxes = new List<vmLabelValuePair>();
        public List<vmLabelValuePair> claimCheckboxes { get { return _claimCheckboxes; } set { _claimCheckboxes = value; } }

        //private List<vmLabelValuePair> _claimDropdowns = new List<vmLabelValuePair>();
        //public List<vmLabelValuePair> claimDropdowns { get { return _claimDropdowns; } set { _claimDropdowns = value; } }

        private List<vmDropdownValueCollection> _claimDropdowns = new List<vmDropdownValueCollection>();
        public List<vmDropdownValueCollection> claimDropdowns { get { return _claimDropdowns; } set { _claimDropdowns = value; } }
    }

    
}