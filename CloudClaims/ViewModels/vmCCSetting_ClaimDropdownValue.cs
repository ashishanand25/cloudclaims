using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmCCSetting_ClaimDropdownValue {
        public int ddlValueID { get; set; }
        public string ddlValueItem { get; set; }
        public int sortOrder { get; set; }
        public bool itemDisabled { get; set; }
        public bool itemDeleted { get; set; }
    }
}